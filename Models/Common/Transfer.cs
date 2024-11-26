public class Transfer : BaseEntityV2
{
    public string Reference { get; set; }
    public Guid TransferFrom { get; set; } //ID?
    public Guid TransferTo { get; set; }  //ID??
    public string TransferStatus { get; set; }
    public List<TransferItem> Items { get; set; }
}