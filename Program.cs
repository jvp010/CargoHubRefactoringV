using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////////////////////////////////////////////////////
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

//////////////////////////////////////////////////////////////////////////////////////////////////////////////

using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("enter in yes to load the jsons");
    string answer = Console.ReadLine();

    if (answer == "yes")
    {
        var context = scope.ServiceProvider.GetRequiredService<ModelContext>();

        string jsonClient = File.ReadAllText("data/clients.json");
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonClient);
        context.Clients.AddRange(clients); // succes

        // ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        string jsonItemLines= File.ReadAllText("data/item_lines.json");                                           
        List<ItemLine> ItemLines = JsonSerializer.Deserialize<List<ItemLine>>(jsonItemLines);
        List<ItemLine> newItemLines = [];
        foreach (var item in ItemLines)
        {
            newItemLines.Add(new ItemLine{id = 0, name = item.name , description = item.description , created_at = item.created_at , updated_at = item.updated_at});
        } 
        context.ItemLines.AddRange(newItemLines);

        string jsonItemGroups= File.ReadAllText("data/item_groups.json");                                           
        List<ItemGroup> ItemGroups = JsonSerializer.Deserialize<List<ItemGroup>>(jsonItemGroups);    
        List<ItemGroup> newItemGroup = [];
        foreach (var item in ItemGroups)
        {
            newItemGroup.Add(new ItemGroup{id = 0, name = item.name , description = item.description , created_at = item.created_at , updated_at = item.updated_at});
        } 
        context.ItemGroups.AddRange(newItemGroup);

        string jsonItemTypes= File.ReadAllText("data/item_types.json");                                           
        List<ItemType> ItemTypes = JsonSerializer.Deserialize<List<ItemType>>(jsonItemTypes);    
        List<ItemType> newItemType = [];
        foreach (var item in ItemTypes)
        {
            newItemType.Add(new ItemType{id = 0, name = item.name , description = item.description , created_at = item.created_at , updated_at = item.updated_at});
        } 
        context.ItemTypes.AddRange(newItemType);  


        string jsonSuppliers = File.ReadAllText("data/suppliers.json");
        List<Supplier> Suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonSuppliers);
        context.Suppliers.AddRange(Suppliers);


        context.SaveChanges();    // succes
        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        string jsonItem = File.ReadAllText("data/items.json");
        List<Item> items = JsonSerializer.Deserialize<List<Item>>(jsonItem);
        context.Items.AddRange(items); // succes
        context.SaveChanges();    // succes



        string jsonWarehouse = File.ReadAllText("data/warehouses.json");
        List<Warehouse> Warehouses = JsonSerializer.Deserialize<List<Warehouse>>(jsonWarehouse);
        context.Warehouses.AddRange(Warehouses); // succes
        context.SaveChanges();


        string jsonLocation = File.ReadAllText("data/locations.json");
        List<Location> locations = JsonSerializer.Deserialize<List<Location>>(jsonLocation);
        context.Locations.AddRange(locations); // succes

        context.SaveChanges();


        string jsonInventory = File.ReadAllText("data/inventories.json");
        List<InventoryTemplate> InventoriesTemplate = JsonSerializer.Deserialize<List<InventoryTemplate>>(jsonInventory);
        List<Inventory> inventories = [];
        foreach (var inventory in InventoriesTemplate)
        {
            List<int> ids = inventory.locations;
            List<Location> locationsholder = [];

            foreach (var id in ids)
            {
                locationsholder.Add(context.Locations.Find(id));
            }
            Inventory NewInventory = new Inventory
            {
                item_id = inventory.item_id,
                description = inventory.description,
                item_reference = inventory.item_reference,
                total_on_hand = inventory.total_on_hand,
                total_expected = inventory.total_expected,
                total_ordered = inventory.total_ordered,
                total_allocated = inventory.total_allocated,
                total_available = inventory.total_available,
                locations = locationsholder

            };
            inventories.Add(NewInventory);
        }



        context.Inventorys.AddRange(inventories); // success
        context.SaveChanges();



        string jsonShipment = File.ReadAllText("data/shipments.json");

        List<Shipment> Shipments = JsonSerializer.Deserialize<List<Shipment>>(jsonShipment);
        context.Shipments.AddRange(Shipments);
        context.SaveChanges();





        string jsonOrder = File.ReadAllText("data/orders.json");
        List<Order> Orders = JsonSerializer.Deserialize<List<Order>>(jsonOrder);
        List<Order> newOrders = new List<Order>();
        foreach (var item in Orders)  // Loop through the original Orders list, not newOrders
        {
            newOrders.Add(new Order
            {
                id = 0,               // Set ID as 0
                bill_to = item.bill_to,  // Set bill_to from each item in Orders
                source_id = item.source_id,
                order_date = item.order_date,
                request_date = item.request_date,
                reference = item.reference,
                reference_extra = item.reference_extra,
                order_status = item.order_status,
                notes = item.notes,
                shipping_notes = item.shipping_notes,
                picking_note = item.picking_note,
                warehouse_id = item.warehouse_id,
                ship_to = item.ship_to,
                shipment_id = item.shipment_id,
                total_amount = item.total_amount,
                total_discount = item.total_discount,
                total_tax = item.total_tax,
                total_surcharge = item.total_surcharge,
                items = new List<OrderItem>(item.items)  // Ensure items list is copied correctly
            });
            
        }
        context.Orders.AddRange(newOrders.Take(6858)); //success

        context.SaveChanges();




        string jsonTransfer = File.ReadAllText("data/transfers.json");
        List<Transfer> Transfers = JsonSerializer.Deserialize<List<Transfer>>(jsonTransfer);
        context.Transfers.AddRange(Transfers); // succes

        context.SaveChanges();


    }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

app.Run();



// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var dbContext = services.GetRequiredService<ModelContext>();
//     string json = @"
//         {
//             ""Name"": ""John Doe"",
//             ""Courses"": [
//                 { ""Title"": ""fuckwad"" },
//                 { ""Title"": ""asd"" }
//             ]
//         }";

//     // Deserialize JSON into Student object
//     var studentFromJson = JsonSerializer.Deserialize<Student>(json);
//     dbContext.Students.AddRange(studentFromJson);

//     dbContext.SaveChanges();
//     Student student = dbContext.Students
//     .Include(s => s.Courses) // Eager load the Courses for the Student
//     .FirstOrDefault(s => s.Id == 7); // Use FirstOrDefault to get the student by id
//     foreach (var item in student.Courses)
//     {
//         System.Console.WriteLine(item.Title);
//     }

