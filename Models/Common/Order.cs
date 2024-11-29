public class Order : BaseEntity
{
    public int source_id { get; set; }
    public DateTime order_date { get; set; }
    public DateTime request_date { get; set; }
    public string reference { get; set; }
    public string reference_extra { get; set; }
    public string order_status { get; set; }
    public string notes { get; set; }
    public string shipping_notes { get; set; }
    public string picking_note { get; set; }
    public int warehouse_id { get; set; }
    public int? ship_to { get; set; } // ID?? CLIENT ID
    public int? bill_to { get; set; } // ID??
    public int? shipment_id { get; set; }
    public decimal total_amount { get; set; }
    public decimal total_discount { get; set; }
    public decimal total_tax { get; set; }
    public decimal total_surcharge { get; set; }
    public List<OrderItem> items { get; set; }
}