using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class OrderItem 
{
    [Key]
    public int id {get;set;}

    [JsonPropertyName("item_id")]  // Ensures the JSON property name maps correctly to C# property
    public string order_item_id { get; set; }
    public int amount { get; set; }
    public int OrderId { get; set; }



}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?
// Stem voor nee - valdier