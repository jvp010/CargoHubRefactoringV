using System.Linq;
using Microsoft.EntityFrameworkCore;

public class CrudService<T> : ICRUDinterface<T> where T : BaseEntity
{
    private readonly ModelContext _context;

    public CrudService(ModelContext context)
    {
        _context = context;
    }

    public bool Delete(int id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Delete(T target)
    {
        var entity = _context.Set<T>().Find(target);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

   public T Get(int id)
{
    T entity = SearchObject<T>.Check(default, _context, id);

    if (entity == null)
    {
        entity = _context.Set<T>().FirstOrDefault(e => e.Id == id);
    }

    return entity;
}


    public List<T> GetAll()
    {
        IQueryable<T> query = _context.Set<T>().AsQueryable();

        // Eager loading for specific types
        if (typeof(T) == typeof(Order) || typeof(T) == typeof(Shipment) || typeof(T) == typeof(Transfer))
        {
            query = query.Include("Items");
        }

        return query.ToList();
    }

    public async Task Patch(T target)
    {
        var entity = await _context.Set<T>().FindAsync(target);
        // Implementation needed
    }

    public T Post(T target)
    {
        if (target != null)
        {
            _context.Set<T>().Add(target);
            _context.SaveChanges();
            return target;
        }
        return null;
    }


    // bool ICRUDinterface<T>.Patch(T target)
    // {
    //     throw new NotImplementedException();
    // }

    public bool Put(T target)
    {
        if (target == null) return false;

        _context.ChangeTracker.Clear();

        _context.Set<T>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }
}