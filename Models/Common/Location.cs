using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Location : BaseEntity
{
    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("inventories")]
    public ICollection<Inventory> Inventories { get; set; }
}
