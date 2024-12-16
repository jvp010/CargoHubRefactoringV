using System.Text.Json.Serialization;

public class Transfer : BaseEntity
{
    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    [JsonPropertyName("transfer_from")]
    public int? TransferFrom { get; set; } // Nullable to represent optional transfer origin.

    [JsonPropertyName("transfer_to")]
    public int TransferTo { get; set; }

    [JsonPropertyName("transfer_status")]
    public string TransferStatus { get; set; }

    [JsonPropertyName("items")]
    public List<TransferItem> Items { get; set; }
}
