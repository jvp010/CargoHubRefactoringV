public class Shipment : BaseEntity
{
    //public int order_id { get; set; }
    public int source_id { get; set; } // deze mocht weg denk ik want het wees verder nergens naar.
    public DateOnly order_date { get; set; } //dateonly??? want ->  "order_date": "2024-05-01",  -> in python versie
    public DateOnly request_date { get; set; } //dateonly??? ...
    public DateOnly shipment_date { get; set; } //dateonly??? ...
    public string shipment_type { get; set; }
    public string shipment_status { get; set; }
    public string notes { get; set; }
    public string carrier_code { get; set; }
    public string carrier_description { get; set; }
    public string service_code { get; set; }
    public string payment_type { get; set; }
    public string transfer_mode { get; set; }
    public int total_package_count { get; set; }
    public decimal total_package_weight { get; set; }
    public List<ShipmentItem> items { get; set; }
}