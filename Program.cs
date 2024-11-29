using System.Text.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////////////////////////////////////////////////////
builder.Services.AddDbContext<ModelContext>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));
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
  
  if(answer == "yes")
  {
        var context = scope.ServiceProvider.GetRequiredService<ModelContext>();

        // string jsonClient = File.ReadAllText("data/clients.json");
        // List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonClient);
        // context.Clients.AddRange(clients); // succes

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        // string jsonItemGroups= File.ReadAllText("data/item_groups.json");                                           //
        // List<ItemGroup> ItemGroups = JsonSerializer.Deserialize<List<ItemGroup>>(jsonItemGroups);                   //
        // bool check = DuplicateCheck(ItemGroups);                                                                    //
        // if(check == true) System.Console.WriteLine("duplicate found");                                              //
        // else System.Console.WriteLine("duplicate not found");                                                       //
                                                                                                                       //  duplicate key error/bug
        // System.Console.WriteLine("enter something to continue");                                                    //
        // string wait = Console.ReadLine();                                                                           //
        // context.ItemGroups.AddRange(ItemGroups); // ergens hier is er een id duplicate                              //
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // string jsonItem = File.ReadAllText("data/items.json");
        // List<Item> items = JsonSerializer.Deserialize<List<Item>>(jsonItem);
        // context.items.AddRange(items); kan pas werken als itemgroup goed is


        
        string jsonOrder = File.ReadAllText("data/orders.json");
        List<Order> Orders = JsonSerializer.Deserialize<List<Order>>(jsonOrder);
        context.orders.AddRange(Orders); // helaas ook fout

        // string jsonInventory = File.ReadAllText("data/inventories.json");
        // List<Inventory> Inventories = JsonSerializer.Deserialize<List<Inventory>>(jsonInventory);
        // context.inventories.AddRange(Inventories); // inventory dont work yet

        
        // string jsonLocation = File.ReadAllText("data/locations.json");
        // List<Location> locations = JsonSerializer.Deserialize<List<Location>>(jsonLocation);
        // context.locations.AddRange(locations); // succes

        


        context.SaveChanges();
  }
  

}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

app.Run();

static bool DuplicateCheck(List<ItemGroup> itemGroups){
    List<int> Ids = [];

    foreach (var item in itemGroups)
    {
        if(Ids.Contains(item.id)) return true;
        Ids.Add(item.id);
    }
    return false;

}