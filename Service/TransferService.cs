
using Microsoft.EntityFrameworkCore;

public class TransferService : CrudService<Transfer>
{
    private readonly ModelContext _context;

    public TransferService(ModelContext context) : base(context)
    {
        _context = context;
    }

    
    public TransferItem? GetItemsInTransfer(int TransferID)
    {
        Transfer? holder = _context.Transfers.FirstOrDefault(x => x.Id == TransferID);
        if (holder == null) return null;
        return holder.Items[0]; // Transfer Item List so far only had always 1 element in there list
    }



   
}