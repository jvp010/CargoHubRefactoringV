
using Microsoft.EntityFrameworkCore;

public class TransferService : CrudService<Transfer>
{
    private readonly ModelContext _context;

    public TransferService(ModelContext context) : base(context)
    {
        _context = context;
    }

    
    public List<TransferItem>? GetItemsInTransfer(int TransferID)
    {
        Transfer? holder = _context.Transfers.FirstOrDefault(x => x.Id == TransferID);
        if (holder == null) return null;
        return holder.Items; 
    }



   
}