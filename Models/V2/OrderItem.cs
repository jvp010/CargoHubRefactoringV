public class OrderItem : BaseEntityV2
{
    public Guid ItemId { get; set; }
    public Guid OrderId {get;set;}
    public int Amount { get; set; }
}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?