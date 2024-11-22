using Microsoft.EntityFrameworkCore;
    public class RandomObject
    {
         public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ModelContext : DbContext
    {
        // DbSets for your entities

        public DbSet<RandomObject> RandomObject { get; set; }




        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
        }
    }

