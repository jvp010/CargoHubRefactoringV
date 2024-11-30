public class Inventory : BaseEntity
{
    public string item_id { get; set; }
    public string description { get; set; }
    public string item_reference { get; set; }
    public ICollection<Location> locations { get; set; }
    public int total_on_hand { get; set; }
    public int total_expected { get; set; }
    public int total_ordered { get; set; }
    public int total_allocated { get; set; }
    public int total_available { get; set; }
}