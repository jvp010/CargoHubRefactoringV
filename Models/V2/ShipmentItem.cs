using System.ComponentModel.DataAnnotations;

public class ShipmentItem{
    [Key]
    public string item_id { get; set; }
    public int amount { get; set; }
    public int ShipmentId { get; set; } // Foreign key to Shipment

    public virtual Shipment Shipment { get; set; } // Navigation property

}