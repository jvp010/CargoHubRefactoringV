using System.Text.Json.Serialization;

public class ItemLine : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
