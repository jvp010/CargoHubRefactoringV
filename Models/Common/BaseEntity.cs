public abstract class BaseEntity
{
    public int id { get; set; }

    public string created_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    public string updated_at { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}
