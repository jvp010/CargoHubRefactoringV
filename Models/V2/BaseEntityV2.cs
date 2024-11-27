public abstract class BaseEntityV2
{
    public Guid Id { get; set; }
    public DateTime CreatedAt = DateTime.UtcNow;
    public DateTime UpdatedAt = DateTime.UtcNow;
}


//proof of concept

// var example =new HumanBot{Name = "Bot"};
// Console.WriteLine(example.date);

// public abstract class Test{
//     public DateTime date = DateTime.UtcNow;
// }

// public class HumanBot : Test{
//     public string Name {get;set;}
// }

