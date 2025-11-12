using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ItemService : ItemInterface
{
    private readonly ModelContext _context;

    public ItemService(ModelContext context)
    {
        _context = context;
    }

    public bool Delete(string id)
    {
        var entity = _context.Set<Item>().FirstOrDefault(x => x.Uid == id);
        if (entity != null)
        {
            _context.Set<Item>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    public bool Delete(Item target)
    {
        var entity = _context.Set<Item>().Find(target.Uid);
        if (entity != null)
        {
            _context.Set<Item>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public Item? Get(string id)
    {
        return _context.Set<Item>().FirstOrDefault(x => x.Uid == id);
    }

    public List<Item> GetAll()
    {
        return _context.Set<Item>().ToList();
    }

    public Item Post(Item target)
    {
        if (target.Uid == "") return null;
        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }
        if (CheckIfTimeIsCorrect(target) == false) return null;
        
        _context.Set<Item>().Add(target);
        _context.SaveChanges();

        return target;
    }

    public bool Put(Item target)
    {
        Item? Old = this.Get(target.Uid);
        // a check to protect the DB from wrongly entered/ not existing ids in item class
        if (target == null || (Old == null)) return false;
       
        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        _context.Set<Item>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }

    public List<Item> GetItemsForItemGroup(int ItemGroupID)
    {

        List<Item> Result = _context.Items.Where(x => x.ItemGroup == ItemGroupID).ToList();

        return Result;
    }

    public List<Item> GetItemsForItemType(int ItemTypeID)
    {

        List<Item> Result = _context.Items.Where(x => x.ItemType == ItemTypeID).ToList();

        return Result;
    }

    public List<Item> GetItemsForItemLine(int ItemLineID)
    {

        List<Item> Result = _context.Items.Where(x => x.ItemLine == ItemLineID).ToList();

        return Result;
    }
    public List<Item> GetItemsForSupplier(int SupplierID)
    {

        List<Item> Result = _context.Items.Where(x => x.SupplierId == SupplierID).ToList();

        return Result;
    }

    private bool CheckIfTimeIsCorrect(Item target)
    {

        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;


    }
}