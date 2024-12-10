using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public string uid { get; set; }
    public string code { get; set; }
    public string description { get; set; }
    public string short_description { get; set; }
    public string upc_code { get; set; }
    public string model_number { get; set; }
    public string commodity_code { get; set; }
    public int? item_line { get; set; } // can be null
    public int? item_group { get; set; } // can be null
    public int? item_type { get; set; } // can be null
    public int unit_purchase_quantity { get; set; }
    public int unit_order_quantity { get; set; }
    public int pack_order_quantity { get; set; }
    public int supplier_id { get; set; } // cannot be deleted if it has references
    public string supplier_code { get; set; }
    public string supplier_part_number { get; set; }
}