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

    public T? Get(int id)
    {
        T? entity = SearchObject<T>.Check(default!, _context, id);

        if (entity == null)
        {
            entity = _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        return entity;
    }


    public List<T> GetAll()
    {
        return _context.Set<T>()
            .OrderBy(x => x.Id)
            .ToList();
    }




    public async Task Patch(T target)
    {
        var entity = await _context.Set<T>().FindAsync(target);
        // Implementation needed
    }

    public T Post(T target)
    {

        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }

        if (CheckIfTimeIsCorrect(target) == false) return null!;
        if (target.Id == 0 & _context.Set<T>().ToList().Count != 0)
        {
            target.Id = _context.Set<T>().OrderBy(x => x.Id).ToList().Last().Id + 1; // autogenereted id when using a large DB

        }
        _context.Set<T>().Add(target);
        _context.SaveChanges();

        return target;

    }
    



    public bool Put(T target)
    {
        T Old = this.Get(target.Id);
        if (target == null || ( Old == null))  return false;

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        _context.Set<T>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }


    private bool CheckIfTimeIsCorrect(T target)
    {

        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;


    }
}