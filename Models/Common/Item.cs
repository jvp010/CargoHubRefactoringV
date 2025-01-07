using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Item
{
    [Key]
    [JsonPropertyName("uid")]
    public string Uid { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; }

    [JsonPropertyName("upc_code")]
    public string UpcCode { get; set; }

    [JsonPropertyName("model_number")]
    public string ModelNumber { get; set; }

    [JsonPropertyName("commodity_code")]
    public string CommodityCode { get; set; }

    [JsonPropertyName("item_line")]
    public int? ItemLine { get; set; } // Nullable

    [JsonPropertyName("item_group")]
    public int? ItemGroup { get; set; } // Nullable

    [JsonPropertyName("item_type")]
    public int? ItemType { get; set; } // Nullable

    [JsonPropertyName("unit_purchase_quantity")]
    public int UnitPurchaseQuantity { get; set; }

    [JsonPropertyName("unit_order_quantity")]
    public int UnitOrderQuantity { get; set; }

    [JsonPropertyName("pack_order_quantity")]
    public int PackOrderQuantity { get; set; }

    [JsonPropertyName("supplier_id")]
    public int SupplierId { get; set; } // Cannot be deleted if referenced

    [JsonPropertyName("supplier_code")]
    public string SupplierCode { get; set; }

    [JsonPropertyName("supplier_part_number")]
    public string SupplierPartNumber { get; set; }
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
