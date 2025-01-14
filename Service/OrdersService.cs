using Microsoft.EntityFrameworkCore;

public class OrderService : CrudService<Order>
{
    private readonly ModelContext _context;

    public OrderService(ModelContext context) : base(context)
    {
        _context = context;
    }

    

    public List<OrderItem>? GetItemsInOrder(int OrderID)
    {
        Order? holder = _context.Orders.FirstOrDefault(x => x.Id == OrderID);
        if (holder == null) return null;
        return holder.Items; // Order Item List so far only had always 1 element in their list
    }

    private bool CheckIfTimeIsCorrect(Order target)
    {
        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;
    }
}
