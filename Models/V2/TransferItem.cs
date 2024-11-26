public class TransferItem : BaseEntityV2
{
    public Guid ItemId { get; set; }
    public Guid TransferId{get;set;}
    public int Amount { get; set; }
}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?