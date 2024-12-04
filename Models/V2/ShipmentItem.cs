using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ShipmentItem{
    [Key]
    public int id {get;set;}
    [JsonPropertyName("item_id")]  // Ensures the JSON property name maps correctly to C# property
    public string shipment_item_id { get; set; }
    public int amount { get; set; }
    public int ShipmentId { get; set; }


}