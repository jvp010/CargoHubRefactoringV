public class InventoryService : CrudService<Inventory>
{
    private readonly ModelContext _context;

    public InventoryService(ModelContext context) : base(context)
    {
        _context = context;
    }
    public override bool Put(Inventory target)
    {
        var holder = target.Locations;
        var holder2 = _context.Locations.Select(l => l.Id).ToList();

        foreach (var location in holder)
        {
            if (!holder2.Contains(location))
            {
                return false;
            }
        }

        this.Put(target);
        return true;
    }

    public List<Inventory> GetInventoriesForItem(string ItemID)
    {
        List<Inventory> inventories = _context.Inventories.Where(x => x.ItemId == ItemID).ToList();
        return inventories;
    }
    public (bool, double[]) GetInventoryTotalsForItem(string ItemID)
    {
        bool Check = false;
        Inventory? CheckifExist = _context.Inventories.FirstOrDefault(x => x.ItemId == ItemID);
        if (CheckifExist != null) Check = true;

        if (Check == false) return (Check, []);

        double[] Result = new double[4];
        var Inventories = _context.Inventories.ToList();
        foreach (var Inventory in Inventories)
        {
            if (Inventory.ItemId == ItemID)
            {
                Result[0] += Inventory.TotalExpected;
                Result[1] += Inventory.TotalOrdered;
                Result[2] += Inventory.TotalAllocated;
                Result[3] += Inventory.TotalAvailable;

            }
        }

        return (Check, Result);

    }

}