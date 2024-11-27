public class OrderItem 
{
    public string item_id { get; set; }
    public int order_id {get;set;}
    public int amount { get; set; }
}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?
// Stem voor nee - valdier