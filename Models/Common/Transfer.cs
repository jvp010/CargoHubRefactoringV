public class Transfer : BaseEntity
{
    public string reference { get; set; }
    public int transfer_from { get; set; } //ID? // tussen inventories? denk dat als we verder zijn dit mss wel achterkomen
    public int transfer_to { get; set; }  //ID??
    public string transfer_status { get; set; }
    public List<TransferItem> items { get; set; }
}