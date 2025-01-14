
using Microsoft.EntityFrameworkCore;

public class LocationService : CrudService<Location>
{
    private readonly ModelContext _context;

    public LocationService(ModelContext context) : base(context)
    {
        _context = context;
    }

    
    public List<Location> GetLocationsInWarehouse(int WarehouseId)
    {
        return _context.Locations.Where(x => x.WarehouseId == WarehouseId).ToList();
    }



    private bool CheckIfTimeIsCorrect(Location target)
    {

        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;


    }
}