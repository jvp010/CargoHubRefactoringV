using Microsoft.EntityFrameworkCore;
    public class ModelContext : DbContext
    {
        // DbSets for your entities

        public DbSet<Client> Client { get; set; }
        public DbSet<Inventory> Inventory {get;set;}
        public DbSet<ItemGroup> ItemGroup {get;set;}
        public DbSet<ItemLine> ItemLine {get;set;}
        public DbSet<ItemType> ItemType {get;set;}
        public DbSet<Item> Item {get;set;}
        public DbSet<Location> Location {get;set;}
        public DbSet<Order> Order {get;set;}
        public DbSet<Shipment> Shipment {get;set;}
        public DbSet<Supplier> Supplier {get;set;}
        public DbSet<Transfer> Transfer {get;set;}
        public DbSet<Warehouse> Warehouse {get;set;}




        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ITEM
            modelBuilder.Entity<Item>()
                .HasKey(i => i.uid); 

            modelBuilder.Entity<Item>()
                .Property(i => i.uid)
                .IsRequired(); 

            modelBuilder.Entity<Item>()
                .HasOne<ItemLine>()  
                .WithMany()          
                .HasForeignKey(i => i.item_line)
                .IsRequired(false);
            
            modelBuilder.Entity<Item>()
                .HasOne<ItemType>()
                .WithMany()
                .HasForeignKey(i => i.item_type)
                .IsRequired(false);
            
            modelBuilder.Entity<Item>()
                .HasOne<ItemGroup>()
                .WithMany()
                .HasForeignKey(i => i.item_group)
                .IsRequired(false);
            
            modelBuilder.Entity<Item>()
                .HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(i => i.supplier_id);

            // Inventory
            modelBuilder.Entity<Inventory>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Inventory>()
                .Property(i => i.id)
                .IsRequired();

            modelBuilder.Entity<Inventory>()
                .HasOne<Item>()
                .WithOne()
                .HasForeignKey<Inventory>(i => i.item_id);

            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.locations)
                .WithMany(l => l.inventories);

            // Location
            modelBuilder.Entity<Location>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Location>()
                .Property(i => i.id)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(l => l.warehouse_id);
            
            //OrdeR
            modelBuilder.Entity<Order>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Order>()
                .Property(i => i.id)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(i => i.warehouse_id);
            
            modelBuilder.Entity<Order>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(i => i.ship_to)
                .IsRequired(false);
        
            modelBuilder.Entity<Order>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(i => i.bill_to)
                .IsRequired(false);
            
            modelBuilder.Entity<Order>()
                .HasOne<Shipment>()
                .WithOne()
                .HasForeignKey<Order>(i => i.shipment_id);

            modelBuilder.Entity<Order>(order =>
            {
                order.OwnsMany(o => o.items, item =>
                {
                    item.Property(i => i.item_id).HasColumnName("item_id");
                    item.Property(i => i.amount).HasColumnName("amount");
                });
            });
            
            // shipment
            modelBuilder.Entity<Shipment>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Shipment>()
                .Property(i => i.id)
                .IsRequired();
                
            // modelBuilder.Entity<Shipment>()    => removed to prevent circular fk restraint
            //     .HasOne<Order>()
            //     .WithOne()
            //     .HasForeignKey<Shipment>(i => i.order_id);

            modelBuilder.Entity<Shipment>(shipment =>
            {
                shipment.OwnsMany(s => s.items, item =>
                {
                    item.Property(i => i.item_id).HasColumnName("item_id");
                    item.Property(i => i.amount).HasColumnName("amount");
                });
            });
            
            // suppleir
            modelBuilder.Entity<Supplier>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Supplier>()
                .Property(i => i.id)
                .IsRequired();
            
            //transfer
            modelBuilder.Entity<Transfer>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Transfer>()
                .Property(i => i.id)
                .IsRequired();
            
            modelBuilder.Entity<Transfer>()
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(i => i.transfer_from)
                .IsRequired(false);
            
            modelBuilder.Entity<Transfer>()
                .HasOne<Location>()
                .WithMany()
                .HasForeignKey(i => i.transfer_to)
                .IsRequired(false);

            modelBuilder.Entity<Transfer>(transfer =>
            {
                // is eigenlijk ``OwnsOne`` omdat in de data file elke tranfer maar 1 transfer item opslaat, 
                // maar omdat het wordt opgeslagen in een list moet het OwnsMany zijn. Is niet net als bij 
                // warehouse wnt daar word contact wel gwn opgeslagen ipv in een list.
                transfer.OwnsMany(t => t.items, item => 
                {
                    item.Property(i => i.item_id).HasColumnName("item_id");
                    item.Property(i => i.amount).HasColumnName("amount");
                });
            });
            
            //warehouse 
            modelBuilder.Entity<Warehouse>()
                .HasKey(i => i.id);
            
            modelBuilder.Entity<Warehouse>()
                .Property(i => i.id)
                .IsRequired();

            modelBuilder.Entity<Warehouse>(warehouse =>
            {
                warehouse.OwnsOne(w => w.contact, contact =>
                {
                    contact.Property(i => i.name).HasColumnName("contact_name");
                    contact.Property(i => i.phone).HasColumnName("contact_phone");
                    contact.Property(i => i.email).HasColumnName("contact_email");
                });
            });

        }
    }

