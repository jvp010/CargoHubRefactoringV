using Microsoft.EntityFrameworkCore;

public class ShipmentService : CrudService<Shipment>
{
    private readonly ModelContext _context;

    public ShipmentService(ModelContext context) : base(context)
    {
        _context = context;
    } // Fixed the missing closing brace here

    public List<ShipmentItem>? GetItemsInShipment(int shipmentId)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        return shipment?.Items;
    }
    public bool UpdateItemsInShipment(int shipmentId, List<ShipmentItem> items)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        if (shipment == null) return false;

        var currentItems = shipment.Items;

        foreach (var currentItem in currentItems)
        {
            var found = items.Any(item => item.shipment_item_id == currentItem.shipment_item_id);
            if (!found)
            {
                var inventories = _context.Inventories.Where(i => i.ItemId == currentItem.shipment_item_id).ToList();
                var maxInventory = inventories.OrderByDescending(i => i.TotalOrdered).FirstOrDefault();
                if (maxInventory != null)
                {
                    maxInventory.TotalOrdered -= currentItem.Amount;
                    maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;
                    _context.Inventories.Update(maxInventory);
                }
            }
        }

        foreach (var currentItem in currentItems)
        {
            foreach (var newItem in items)
            {
                if (currentItem.shipment_item_id == newItem.shipment_item_id)
                {
                    var inventories = _context.Inventories.Where(i => i.ItemId == currentItem.shipment_item_id).ToList();
                    var maxInventory = inventories.OrderByDescending(i => i.TotalOrdered).FirstOrDefault();
                    if (maxInventory != null)
                    {
                        maxInventory.TotalOrdered += newItem.Amount - currentItem.Amount;
                        maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;
                        _context.Inventories.Update(maxInventory);
                    }
                }
            }
        }

        shipment.Items = items;
        return this.put(shipment);
        
    

        foreach (var item in target.Items)
        {
            if (_context.Items.FirstOrDefault(x => x.Uid == item.shipment_item_id) == null) return null!;
        }  // if item.uid does not exist in items return null

        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }

        if (CheckIfTimeIsCorrect(target) == false) return null!;
        if (target.Id == 0 & _context.Set<Shipment>().ToList().Count != 0)
        {
            target.Id = _context.Set<Shipment>().OrderBy(x => x.Id).ToList().Last().Id + 1; // autogenereted id when using a large DB
        }
        _context.Set<Shipment>().Add(target);
        _context.SaveChanges();

        return target;
    }

    public override bool Put(Shipment target)
    {
        if (_context.Orders.FirstOrDefault(x => x.Id == target.OrderId) == null) return null!;

        Shipment? Old = this.Get(target.Id);
        if (target == null || (Old == null)) return false;

        foreach (var item in target.Items)
        {
            if (_context.Items.FirstOrDefault(x => x.Uid == item.shipment_item_id) == null) return false!;
        }   // if item.uid does not exist in items return null

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        target.ShipmentStatus = "Scheduled";
        _context.Set<Shipment>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }

    public ShipmentItem? GetItemsInShipment(int ShipmentID)
    {
        Shipment? holder = _context.Shipments.FirstOrDefault(x => x.Id == ShipmentID);
        if (holder == null) return null;
        return holder.Items[0]; // Shipment Item List so far only had always 1 element in their list
    }

    private bool CheckIfTimeIsCorrect(Shipment target)
    {
        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;
    }
}
