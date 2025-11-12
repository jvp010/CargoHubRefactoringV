using Microsoft.EntityFrameworkCore;

public class ShipmentService : CrudService<Shipment>
{
    private readonly ModelContext _context;

    public ShipmentService(ModelContext context) : base(context)
    {
        _context = context;
    } 

    public List<ShipmentItem>? GetItemsInShipment(int shipmentId)
    {
        var shipment = _context.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        return shipment?.Items;
    }
    public bool UpdateItemsInShipment(int shipmentId, List<ShipmentItem> items)
    {

        var shipment = _context.Shipments.FirstOrDefault(s => s.Id == shipmentId);
        if (shipment == null) return false;

        // Separate found and not found items
        var foundItems = new List<ShipmentItem>();
        var notFoundItems = new List<ShipmentItem>();

        foreach (var item in shipment.Items)
        {
            var newItem = items.FirstOrDefault(i => i.id == item.id);
            if (newItem != null)
            {
                foundItems.Add(item); 
            }
            else
            {
                notFoundItems.Add(item); 
            }
        }

        foreach (var item in notFoundItems)
        {
            List<Inventory> inventories = _context.Inventories.Where(x => x.ItemId == item.shipment_item_id).ToList();
            Inventory? maxInventory = inventories.OrderByDescending(x => x.TotalOrdered).FirstOrDefault();

            if (maxInventory != null)
            {
                maxInventory.TotalOrdered -= item.amount;
                maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;

                // Update inventory
                var inventoryService = new InventoryService(_context);
                inventoryService.Put(maxInventory);
            }
        }

        foreach (var newItem in items)
        {
            var currentItem = shipment.Items.FirstOrDefault(x => x.id == newItem.id);
            if (currentItem != null)
            {
                List<Inventory> inventories = _context.Inventories.Where(x => x.ItemId == currentItem.shipment_item_id).ToList();
                Inventory? maxInventory = inventories.OrderByDescending(x => x.TotalOrdered).FirstOrDefault();

                if (maxInventory != null)
                {
                    maxInventory.TotalOrdered += newItem.amount - currentItem.amount;
                    maxInventory.TotalExpected = maxInventory.TotalOnHand + maxInventory.TotalOrdered;

                    var inventoryService = new InventoryService(_context);
                    inventoryService.Put(maxInventory);
                }
            }
        }

        shipment.Items = items;
        this.Put(shipment);
        _context.SaveChanges();

        return true;


    }

    public override bool Put(Shipment target)
    {
        if (_context.Orders.FirstOrDefault(x => x.Id == target.OrderId) == null) return false;

        Shipment? Old = this.Get(target.Id);
        if (target == null || (Old == null)) return false;

        foreach (var item in target.Items)
        {
            if (_context.Items.FirstOrDefault(x => x.Uid == item.shipment_item_id) == null) return false!;
        }

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        target.ShipmentStatus = "Scheduled";
        _context.Set<Shipment>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }

   

    private bool CheckIfTimeIsCorrect(Shipment target)
    {
        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;
    }

   
    
}
