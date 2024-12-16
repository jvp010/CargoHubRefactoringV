using Microsoft.EntityFrameworkCore;

public static class SearchObject<T>
{
    public static T Check(T model, ModelContext _context, int id)
    {
        // Use type-checking to handle specific types
        if (typeof(T) == typeof(Order))
        {
            var order = _context.Set<Order>()
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
            return (T)(object)order;
        }
        else if (typeof(T) == typeof(Shipment))
        {
            var shipment = _context.Set<Shipment>()
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
            return (T)(object)shipment;
        }
        else if (typeof(T) == typeof(Transfer))
        {
            var transfer = _context.Set<Transfer>()
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);
            return (T)(object)transfer;
        }

        return default; // Return null for unsupported types
    }
}