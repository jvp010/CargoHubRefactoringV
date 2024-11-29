using System.Text.Json.Serialization;

public class ItemGroup : BaseEntity
{    
    public string? name { get; set; }
    public string description { get; set; }
}