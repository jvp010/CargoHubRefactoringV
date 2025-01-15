using Microsoft.EntityFrameworkCore;
public class ModelContext : DbContext
{

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }

    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<TransferItem> TransferItems { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<Item> Items { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<ItemGroup> ItemGroups { get; set; }

    public DbSet<ItemLine> ItemLines { get; set; }

    public DbSet<ItemType> ItemTypes { get; set; }

    public DbSet<Inventory> Inventories { get; set; }

    public DbSet<Client> Clients { get; set; }




    public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ITEM // succes
        modelBuilder.Entity<Item>()
            .HasKey(i => i.Uid);

        modelBuilder.Entity<Item>()
            .Property(i => i.Uid)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .HasOne<ItemLine>()
            .WithMany()
            .HasForeignKey(i => i.ItemLine)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Item>()
            .HasOne<ItemType>()
            .WithMany()
            .HasForeignKey(i => i.ItemType)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Item>()
            .HasOne<ItemGroup>()
            .WithMany()
            .HasForeignKey(i => i.ItemGroup)
            .OnDelete(DeleteBehavior.SetNull) 
            .IsRequired(false);

        modelBuilder.Entity<Item>()
            .HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(i => i.SupplierId)
            .OnDelete(DeleteBehavior.Restrict); 

        // Inventory // succes
        modelBuilder.Entity<Inventory>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Inventory>()
            .Property(i => i.Id)
            .IsRequired();

        modelBuilder.Entity<Inventory>()
            .HasOne<Item>()
            .WithMany()
            .HasForeignKey(i => i.ItemId)
            .OnDelete(DeleteBehavior.Restrict); 

    

        // Location // succes
        modelBuilder.Entity<Location>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Location>()
            .Property(i => i.Id)
            .IsRequired();

        modelBuilder.Entity<Location>()
            .HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(i => i.WarehouseId) 
            .OnDelete(DeleteBehavior.Restrict);

        //OrdeR
        modelBuilder.Entity<Order>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Order>()
            .Property(i => i.Id)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.SetNull);


        modelBuilder.Entity<Order>()
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(i => i.ShipTo)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Order>()
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(i => i.BillTo)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Order>()
            .HasOne<Shipment>()
            .WithOne()
            .HasForeignKey<Order>(i => i.ShipmentId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Order>(order =>
        {
            order.OwnsMany(o => o.Items, item =>
            {
                item.Property(i => i.OrderItemId).HasColumnName("item_id");
                item.Property(i => i.Amount).HasColumnName("amount");
            });
        });

        // shipment
        modelBuilder.Entity<Shipment>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Shipment>()
            .Property(i => i.Id)
            .IsRequired();

        // modelBuilder.Entity<Shipment>()    => removed to prevent circular fk restraint
        //     .HasOne<Order>()
        //     .WithOne()
        //     .HasForeignKey<Shipment>(i => i.order_id);

        modelBuilder.Entity<Shipment>(shipment =>
        {
            shipment.OwnsMany(s => s.Items, item =>
            {
                item.Property(i => i.shipment_item_id).HasColumnName("item_id");
                item.Property(i => i.amount).HasColumnName("amount");
            });
        });

        // Supplier
        modelBuilder.Entity<Supplier>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Supplier>()
            .Property(i => i.Id)
            .IsRequired();

        // //transfer
        modelBuilder.Entity<Transfer>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Transfer>()
            .Property(i => i.Id)
            .IsRequired();

        modelBuilder.Entity<Transfer>()
            .HasOne<Location>()
            .WithMany()
            .HasForeignKey(i => i.TransferFrom)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Transfer>()
            .HasOne<Location>()
            .WithMany()
            .HasForeignKey(i => i.TransferTo)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Transfer>(transfer =>
        {
            // is eigenlijk ``OwnsOne`` omdat in de data file elke tranfer maar 1 transfer item opslaat, 
            // maar omdat het wordt opgeslagen in een list moet het OwnsMany zijn. Is niet net als bij 
            // warehouse wnt daar word contact wel gwn opgeslagen ipv in een list.
            transfer.OwnsMany(t => t.Items, item =>
            {
                item.Property(i => i.tranfer_item_id).HasColumnName("item_id");
                item.Property(i => i.amount).HasColumnName("amount");
            });
        });

        //warehouse 
        modelBuilder.Entity<Warehouse>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Warehouse>()
            .Property(i => i.Id)
            .IsRequired();

        modelBuilder.Entity<Warehouse>(warehouse =>
        {
            warehouse.OwnsOne(w => w.Contact, contact =>
            {
                contact.Property(i => i.Name).HasColumnName("contact_name");
                contact.Property(i => i.Phone).HasColumnName("contact_phone");
                contact.Property(i => i.Email).HasColumnName("contact_email");
            });
        });

        modelBuilder.Entity<Client>()
        .Property(c => c.Id)
        .ValueGeneratedOnAdd(); 

    }
}
