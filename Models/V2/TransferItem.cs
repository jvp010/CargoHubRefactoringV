using System.ComponentModel.DataAnnotations;

public class TransferItem 
{
    [Key]
    public string item_id { get; set; }
    public int amount { get; set; }
}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?