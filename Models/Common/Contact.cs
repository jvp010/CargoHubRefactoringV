using System.Text.Json.Serialization;

public class Contact
{
    // [JsonPropertyName("id")]
    // public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}
