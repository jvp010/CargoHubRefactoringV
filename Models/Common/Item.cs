public class Item : BaseEntityV2
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string UpcCode { get; set; }
    public string ModelNumber { get; set; }
    public string CommodityCode { get; set; }
    public Guid? ItemLineId { get; set; } // can be null
    public Guid? ItemGroupId { get; set; } // can be null
    public Guid? ItemTypeId { get; set; } // can be null
    public int UnitPurchaseQuantity { get; set; }
    public int UnitOrderQuantity { get; set; }
    public int PackOrderQuantity { get; set; }
    public Guid SupplierId { get; set; } // cannot be deleted if it has references
    public string SupplierCode { get; set; }
    public string SupplierPartNumber { get; set; }
}