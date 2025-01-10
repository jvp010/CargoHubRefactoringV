
using Microsoft.EntityFrameworkCore;

public class LocationService : CrudService<Location>
{
    private readonly ModelContext _context;

    public LocationService(ModelContext context) : base(context)
    {
        _context = context;
    }

    public override Location Post(Location target)
    {
        if(_context.Warehouses.FirstOrDefault(x => x.Id == target.WarehouseId) == null) return null!;

        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }

        if (CheckIfTimeIsCorrect(target) == false) return null!;
        if (target.Id == 0 & _context.Set<Location>().ToList().Count != 0)
        {
            target.Id = _context.Set<Location>().OrderBy(x => x.Id).ToList().Last().Id + 1; // autogenereted id when using a large DB
        }
        _context.Set<Location>().Add(target);
        _context.SaveChanges();

        return target;
    }

    public virtual bool Put(Location target)
    {
        Location? Old = this.Get(target.Id);
        if (target == null || (Old == null)) return false;
        else if(_context.Warehouses.FirstOrDefault(x => x.Id == target.WarehouseId) == null) return false;

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _context.Set<Location>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
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