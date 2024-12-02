using System.Runtime.InteropServices;

public class Warehouse : BaseEntity
{
    public string code { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string zip { get; set; }
    public string city { get; set; }
    public string province { get; set; }
    public string country { get; set; }
    public Contact contact { get; set; } // todo
}

