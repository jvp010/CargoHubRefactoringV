public class Inventory : BaseEntityV2
{
    public Guid ItemId { get; set; }
    public string Description { get; set; }
    public string ItemReference { get; set; }
    public Guid LocationId { get; set; }
    public int TotalOnHand { get; set; }
    public int TotalExpected { get; set; }
    public int TotalOrdered { get; set; }
    public int TotalAllocated { get; set; }
    public int TotalAvailable { get; set; }
}