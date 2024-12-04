using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class TransferItem 
{
    [Key]
    public int id {get;set;}

    [JsonPropertyName("item_id")]  // Ensures the JSON property name maps correctly to C# property
    public string tranfer_item_id { get; set; }
    public int amount { get; set; }
    public int TransferId { get; set; }

}

// BaseEntity includes create and update at while the ERD model doesnt. include or not?