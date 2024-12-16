using System.Text.Json.Serialization;

public class Supplier : BaseEntity
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("address_extra")]
    public string AddressExtra { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("zip_code")]
    public string ZipCode { get; set; }

    [JsonPropertyName("province")]
    public string Province { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("contact_name")]
    public string ContactName { get; set; }

    [JsonPropertyName("phonenumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("reference")]
    public string Reference { get; set; }
}
