using System.Text.Json.Serialization;

public class ItemType : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
