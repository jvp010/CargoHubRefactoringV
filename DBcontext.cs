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
    
        }
    }

