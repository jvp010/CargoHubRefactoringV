using System.Text.Json.Serialization;

public class Warehouse : BaseEntity
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("zip")]
    public string Zip { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("contact")]
    public Contact Contact { get; set; } 
}
