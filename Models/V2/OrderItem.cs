using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class OrderItem
{
    [Key]
    public int Id { get; set; }  // Primary Key

    [JsonPropertyName("item_id")]  // Maps JSON field name `item_id` to the C# property
    public string OrderItemId { get; set; }

    [JsonPropertyName("amount")]  // Maps JSON field name `amount` to the C# property
    public int Amount { get; set; }

    [JsonPropertyName("order_id")]  // Maps JSON field name `order_id` to the C# property
    public int OrderId { get; set; }
}
