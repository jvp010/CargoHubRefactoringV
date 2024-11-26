public class Shipment : BaseEntityV2
{
    public Guid OrderId { get; set; }
    public Guid SourceId { get; set; } // deze mocht weg denk ik want het wees verder nergens naar.
    public DateTime OrderDate { get; set; } //dateonly??? want ->  "order_date": "2024-05-01",  -> in python versie
    public DateTime RequestDate { get; set; } //dateonly??? ...
    public DateTime ShipmentDate { get; set; } //dateonly??? ...
    public string ShipmentType { get; set; }
    public string ShipmentStatus { get; set; }
    public string Notes { get; set; }
    public string CarrierCode { get; set; }
    public string CarrierDescription { get; set; }
    public string ServiceCode { get; set; }
    public string PaymentType { get; set; }
    public string TransferMode { get; set; }
    public int TotalPackageCount { get; set; }
    public decimal TotalPackageWeight { get; set; }
    public List<ShipmentItem> Items { get; set; }
}