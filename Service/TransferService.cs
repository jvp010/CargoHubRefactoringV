
using Microsoft.EntityFrameworkCore;

public class TransferService : CrudService<Transfer>
{
    private readonly ModelContext _context;

    public TransferService(ModelContext context) : base(context)
    {
        _context = context;
    }

    public override Transfer Post(Transfer target)
    {
        foreach (var item in target.Items)
        {
            if (_context.Items.FirstOrDefault(x => x.Uid == item.tranfer_item_id) == null) return null!;
        }  // if item.uid does not exist in items return null


        if (target.CreatedAt == "" & target.UpdatedAt == "")
        {
            string time = DateTime.UtcNow.ToString();
            target.CreatedAt = time;
            target.UpdatedAt = time;
        }

        if (CheckIfTimeIsCorrect(target) == false) return null!;
        if (target.Id == 0 & _context.Set<Transfer>().ToList().Count != 0)
        {
            target.Id = _context.Set<Transfer>().OrderBy(x => x.Id).ToList().Last().Id + 1; // autogenereted id when using a large DB
        }
        _context.Set<Transfer>().Add(target);
        _context.SaveChanges();

        return target;
    }

    public virtual bool Put(Transfer target)
    {
        Transfer? Old = this.Get(target.Id);
        if (target == null || (Old == null)) return false;

        foreach (var item in target.Items)
        {
            if (_context.Items.FirstOrDefault(x => x.Uid == item.tranfer_item_id) == null) return false!;
        }   // if item.uid does not exist in items return null

        _context.ChangeTracker.Clear();
        target.CreatedAt = Old.CreatedAt;
        target.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        target.TransferStatus = "Scheduled";
        _context.Set<Transfer>().Attach(target);
        _context.Entry(target).State = EntityState.Modified;

        _context.SaveChanges();
        return true;
    }
    public TransferItem? GetItemsInTransfer(int TransferID)
    {
        Transfer? holder = _context.Transfers.FirstOrDefault(x => x.Id == TransferID);
        if (holder == null) return null;
        return holder.Items[0]; // Transfer Item List so far only had always 1 element in there list
    }



    private bool CheckIfTimeIsCorrect(Transfer target)
    {

        bool CheckCreatedAt = DateTime.TryParse(target.CreatedAt, out DateTime parsedDate);
        bool CheckUpdatedAt = DateTime.TryParse(target.UpdatedAt, out DateTime parsedDate2);

        if (CheckCreatedAt == true & CheckUpdatedAt == true) return true;
        return false;


    }
}