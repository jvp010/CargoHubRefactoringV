using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Order : BaseEntity
{
    [JsonPropertyName("source_id")]
    public int SourceId { get; set; }

    [JsonPropertyName("order_date")]
    public DateTime OrderDate { get; set; }

    [JsonPropertyName("request_date")]
    public DateTime RequestDate { get; set; }

    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    [JsonPropertyName("reference_extra")]
    public string ReferenceExtra { get; set; }

    [JsonPropertyName("order_status")]
    public string OrderStatus { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("shipping_notes")]
    public string ShippingNotes { get; set; }

    [JsonPropertyName("picking_note")]
    public string? PickingNote { get; set; }

    [JsonPropertyName("warehouse_id")]
    public int WarehouseId { get; set; }

    [JsonPropertyName("ship_to")]
    public int? ShipTo { get; set; } // Client ID?

    [JsonPropertyName("bill_to")]
    public int? BillTo { get; set; } // Client ID?

    [JsonPropertyName("shipment_id")]
    public int? ShipmentId { get; set; }

    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("total_discount")]
    public decimal TotalDiscount { get; set; }

    [JsonPropertyName("total_tax")]
    public decimal TotalTax { get; set; }

    [JsonPropertyName("total_surcharge")]
    public decimal TotalSurcharge { get; set; }

    [JsonPropertyName("items")]
    public List<OrderItem> Items { get; set; }
}
