using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllers();
builder.Services.AddAuthentication();
//PersonContext added as a scoped (default of method AddDbContext) service:

builder.Services.AddTransient<ICRUDinterface<Client>, CrudService<Client>>();
builder.Services.AddTransient<InventoryService>();

builder.Services.AddTransient<ICRUDinterface<ItemGroup>, CrudService<ItemGroup>>();
builder.Services.AddTransient<ICRUDinterface<ItemLine>, CrudService<ItemLine>>();
builder.Services.AddTransient<ICRUDinterface<ItemType>, CrudService<ItemType>>();
builder.Services.AddTransient<ItemInterface, ItemService>();

builder.Services.AddTransient<ICRUDinterface<Location>, CrudService<Location>>();
builder.Services.AddTransient<ICRUDinterface<Order>, CrudService<Order>>();
builder.Services.AddTransient<ICRUDinterface<Shipment>, CrudService<Shipment>>();
builder.Services.AddTransient<ICRUDinterface<Supplier>, CrudService<Supplier>>();
builder.Services.AddTransient<ICRUDinterface<Transfer>, CrudService<Transfer>>();
builder.Services.AddTransient<ICRUDinterface<Warehouse>, CrudService<Warehouse>>();

builder.Services.AddDbContext<ModelContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"))
                       .EnableSensitiveDataLogging());  // Enable detailed logging
// postgres DB

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Enter 'yes' to load the JSONs.");
    string answer = Console.ReadLine();

    if (answer?.ToLower() == "yes")
    {
        var context = scope.ServiceProvider.GetRequiredService<ModelContext>();

        // Load Client Data
        string jsonClients = File.ReadAllText("data/clients.json");
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonClients);
        context.Clients.AddRange(clients);

        // Load Item Lines
        string jsonItemLines = File.ReadAllText("data/item_lines.json");
        List<ItemLine> itemLines = JsonSerializer.Deserialize<List<ItemLine>>(jsonItemLines);
        List<ItemLine> newItemLines = new();
        foreach (var itemLine in itemLines)
        {
            newItemLines.Add(new ItemLine
            {
                Id = 0,
                Name = itemLine.Name,
                Description = itemLine.Description,
                CreatedAt = itemLine.CreatedAt,
                UpdatedAt = itemLine.UpdatedAt
            });
        }
        context.ItemLines.AddRange(newItemLines);

        // Load Item Groups
        string jsonItemGroups = File.ReadAllText("data/item_groups.json");
        List<ItemGroup> itemGroups = JsonSerializer.Deserialize<List<ItemGroup>>(jsonItemGroups);
        List<ItemGroup> newItemGroups = new();
        foreach (var itemGroup in itemGroups)
        {
            newItemGroups.Add(new ItemGroup
            {
                Id = 0,
                Name = itemGroup.Name,
                Description = itemGroup.Description,
                CreatedAt = itemGroup.CreatedAt,
                UpdatedAt = itemGroup.UpdatedAt
            });
        }
        context.ItemGroups.AddRange(newItemGroups);

        // Load Item Types
        string jsonItemTypes = File.ReadAllText("data/item_types.json");
        List<ItemType> itemTypes = JsonSerializer.Deserialize<List<ItemType>>(jsonItemTypes);
        List<ItemType> newItemTypes = new();
        foreach (var itemType in itemTypes)
        {
            newItemTypes.Add(new ItemType
            {
                Id = 0,
                Name = itemType.Name,
                Description = itemType.Description,
                CreatedAt = itemType.CreatedAt,
                UpdatedAt = itemType.UpdatedAt
            });
        }
        context.ItemTypes.AddRange(newItemTypes);

        // Load Suppliers
        string jsonSuppliers = File.ReadAllText("data/suppliers.json");
        List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonSuppliers);
        context.Suppliers.AddRange(suppliers);

        context.SaveChanges();

        // Load Items
        string jsonItems = File.ReadAllText("data/items.json");
        List<Item> items = JsonSerializer.Deserialize<List<Item>>(jsonItems);
        context.Items.AddRange(items);
        context.SaveChanges();

        // Load Warehouses
        string jsonWarehouses = File.ReadAllText("data/warehouses.json");
        List<Warehouse> warehouses = JsonSerializer.Deserialize<List<Warehouse>>(jsonWarehouses);
        context.Warehouses.AddRange(warehouses);
        context.SaveChanges();

        // Load Locations
        string jsonLocations = File.ReadAllText("data/locations.json");
        List<Location> locations = JsonSerializer.Deserialize<List<Location>>(jsonLocations);
        context.Locations.AddRange(locations);
        context.SaveChanges();

        // Load Inventory Templates and Map to Inventory
        string jsonInventoryTemplates = File.ReadAllText("data/inventories.json");
        List<Inventory> inventories = JsonSerializer.Deserialize<List<Inventory>>(jsonInventoryTemplates);
        context.Inventories.AddRange(inventories);
        context.SaveChanges();

        // Load Shipments
        string jsonShipments = File.ReadAllText("data/shipments.json");
        List<Shipment> shipments = JsonSerializer.Deserialize<List<Shipment>>(jsonShipments);
        context.Shipments.AddRange(shipments);
        context.SaveChanges();

        // Load Orders with Adjusted Values
        string jsonOrders = File.ReadAllText("data/orders.json");
        List<Order> orders = JsonSerializer.Deserialize<List<Order>>(jsonOrders);
        List<Order> mappedOrders = new();
        foreach (var order in orders)
        {
            mappedOrders.Add(new Order
            {
                Id = 0,
                BillTo = order.BillTo,
                SourceId = order.SourceId,
                OrderDate = order.OrderDate,
                RequestDate = order.RequestDate,
                Reference = order.Reference,
                ReferenceExtra = order.ReferenceExtra,
                OrderStatus = order.OrderStatus,
                Notes = order.Notes,
                ShippingNotes = order.ShippingNotes,
                PickingNote = order.PickingNote,
                WarehouseId = order.WarehouseId,
                ShipTo = order.ShipTo,
                ShipmentId = order.ShipmentId,
                TotalAmount = order.TotalAmount,
                TotalDiscount = order.TotalDiscount,
                TotalTax = order.TotalTax,
                TotalSurcharge = order.TotalSurcharge,
                Items = new List<OrderItem>(order.Items)
            });
        }

        context.Orders.AddRange(mappedOrders.Take(6858));
        context.SaveChanges();

        // Load Transfers
        string jsonTransfers = File.ReadAllText("data/transfers.json");
        List<Transfer> transfers = JsonSerializer.Deserialize<List<Transfer>>(jsonTransfers);
        context.Transfers.AddRange(transfers);
        context.SaveChanges();
    }
}

app.Run();