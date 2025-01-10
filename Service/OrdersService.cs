using Microsoft.EntityFrameworkCore;

public class OrderService : CrudService<Order>
{
    private readonly ModelContext _context;

    public OrderService(ModelContext context) : base(context)
    {
        _context = context;
    }

    public override Order Post(Order target)
    {
        if(_context.Warehouses.FirstOrDefault(x => x.Id == target.WarehouseId) == null) return null!;
        else if(_context.Shipments.FirstOrDefault(x => x.Id == target.ShipmentId) == null) return null!;
        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }

        if (CheckIfTimeIsCorrect(target) == false) return null!;
        if (target.Id == 0 & _context.Set<Order>().ToList().Count != 0)
        {
            target.Id = _context.Set<Order>().OrderBy(x => x.Id).ToList().Last().Id + 1; // autogenereted id when using a large DB
        }
        _context.Set<Order>().Add(target);
        _context.SaveChanges();

        return target;
    }

    public virtual bool Put(Order target)
    {
        Order? Old = this.Get(target.Id);
        if (target == null || (Old == null)) return false;
        else if(_context.Warehouses.FirstOrDefault(x => x.Id == target.WarehouseId) == null) return false!;
        else if(_context.Shipments.FirstOrDefault(x => x.Id == target.ShipmentId) == null) return false!;
         // if item.uid does not exist in items return null

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        target.OrderStatus = "Scheduled";
        _context.Set<Order>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }

    public OrderItem? GetItemsInOrder(int OrderID)
    {
        Order? holder = _context.Orders.FirstOrDefault(x => x.Id == OrderID);
        if (holder == null) return null;
        return holder.Items[0]; // Order Item List so far only had always 1 element in their list
    }

    private bool CheckIfTimeIsCorrect(Order target)
    {
        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;
    }
}
