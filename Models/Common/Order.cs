public class Order : BaseEntityV2
{
    public Guid SourceId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequestDate { get; set; }
    public string Reference { get; set; }
    public string ReferenceExtra { get; set; }
    public string OrderStatus { get; set; }
    public string Notes { get; set; }
    public string ShippingNotes { get; set; }
    public string PickingNotes { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ShipTo { get; set; } // ID?? CLIENT ID
    public Guid BillTo { get; set; } // ID??
    public Guid ShipmentId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalSurcharge { get; set; }
    public List<OrderItem> Items { get; set; }
}