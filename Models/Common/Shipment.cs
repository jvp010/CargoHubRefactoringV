using System.Text.Json.Serialization;

public class Shipment : BaseEntity
{
    // [JsonPropertyName("order_id")] // Uncomment if you have an order_id to map.
    // public int OrderId { get; set; }

    [JsonPropertyName("source_id")]
    public int SourceId { get; set; }

    [JsonPropertyName("order_date")]
    public DateOnly OrderDate { get; set; }

    [JsonPropertyName("request_date")]
    public DateOnly RequestDate { get; set; }

    [JsonPropertyName("shipment_date")]
    public DateOnly ShipmentDate { get; set; }

    [JsonPropertyName("shipment_type")]
    public string ShipmentType { get; set; }

    [JsonPropertyName("shipment_status")]
    public string ShipmentStatus { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("carrier_code")]
    public string CarrierCode { get; set; }

    [JsonPropertyName("carrier_description")]
    public string CarrierDescription { get; set; }

    [JsonPropertyName("service_code")]
    public string ServiceCode { get; set; }

    [JsonPropertyName("payment_type")]
    public string PaymentType { get; set; }

    [JsonPropertyName("transfer_mode")]
    public string TransferMode { get; set; }

    [JsonPropertyName("total_package_count")]
    public int TotalPackageCount { get; set; }

    [JsonPropertyName("total_package_weight")]
    public decimal TotalPackageWeight { get; set; }

    [JsonPropertyName("items")]
    public List<ShipmentItem> Items { get; set; }
}
