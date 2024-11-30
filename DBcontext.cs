using Microsoft.EntityFrameworkCore;
    public class ModelContext : DbContext
    {
        // DbSets for your entities

        public DbSet<Client> Clients { get; set; }
        public DbSet<Inventory> inventories {get;set;}
        public DbSet<ItemGroup> ItemGroups {get;set;}
        public DbSet<ItemLine> itemLines {get;set;}
        public DbSet<ItemType> itemTypes {get;set;}
        public DbSet<Item> items {get;set;}
        public DbSet<Location> locations {get;set;}
        public DbSet<Order> orders {get;set;}
        public DbSet<OrderItem> orderItems {get;set;}
        public DbSet<Shipment> shipments {get;set;}
        public DbSet<ShipmentItem> shipmentItems {get;set;}
        public DbSet<Supplier> suppliers {get;set;}
        public DbSet<Transfer> transfers {get;set;}
        public DbSet<TransferItem> transferItems {get;set;}
        public DbSet<Warehouse> warehouses {get;set;}




        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Warehouse>()
            // .OwnsOne(w => w.contact); // Mark Contact as owned by Warehouse

            // modelBuilder.Entity<Item>()
            // .HasKey(i => i.uid); // Specify the primary key

            //  modelBuilder.Entity<OrderItem>()
            // .HasNoKey();  // Make the entity keyless

            // modelBuilder.Entity<Order>()
            // .HasMany(o => o.items)  // Order has many OrderItems
            // .WithOne()  // OrderItem has one Order (no navigation property in OrderItem)
            // .HasForeignKey(oi => oi.order_id);  // Foreign key is order_id in OrderItem

            modelBuilder.Entity<Item>()
            .HasOne<ItemType>()                // Each Item belongs to one ItemGroup
            .WithMany()                          // An ItemGroup can have many Items
            .HasForeignKey(i => i.item_type)    // Foreign key on Item
            .OnDelete(DeleteBehavior.Restrict);  // Restrict delete if referenced (you can change this as per your need)

            modelBuilder.Entity<Item>()
            .HasOne<ItemGroup>()                // Each Item belongs to one ItemGroup
            .WithMany()                          // An ItemGroup can have many Items
            .HasForeignKey(i => i.item_group)    // Foreign key on Item
            .OnDelete(DeleteBehavior.Restrict);  // Restrict delete if referenced (you can change this as per your need)

            modelBuilder.Entity<Item>()
            .HasOne<ItemLine>()                // Each Item belongs to one ItemGroup
            .WithMany()                          // An ItemGroup can have many Items
            .HasForeignKey(i => i.item_line)    // Foreign key on Item
            .OnDelete(DeleteBehavior.Restrict);  // Restrict delete if referenced (you can change this as per your need)

            // //
            // modelBuilder.Entity<ItemLine>()
            // .Property(p => p.id)
            // .ValueGeneratedNever();

            // modelBuilder.Entity<ItemGroup>()
            // .Property(p => p.id)
            // .ValueGeneratedNever();

            // modelBuilder.Entity<ItemType>()
            // .Property(p => p.id)
            // .ValueGeneratedNever();
            // // stopt de auto - increment van de id waardoor het laden van de jsons niet voor id conflicten zorgen 
        }
    }

