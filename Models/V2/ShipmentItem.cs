using System.ComponentModel.DataAnnotations;

public class ShipmentItem{
    [Key]
    public string item_id { get; set; }
    public int amount { get; set; }
}