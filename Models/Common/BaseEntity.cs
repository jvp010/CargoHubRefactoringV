public abstract class BaseEntity
{
    public int id { get; set; }
    public DateTime created_at = DateTime.UtcNow;
    public DateTime updated_at = DateTime.UtcNow;
}
