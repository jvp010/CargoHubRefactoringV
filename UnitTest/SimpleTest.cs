using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
public class CRUDTest
{
    private ModelContext Context;
    public CRUDTest()
    {

        var options = new DbContextOptionsBuilder<ModelContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
        .Options;

        Context = new ModelContext(options);

    }

    [Fact]
    public void GetTest()
    {

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "Group 1",
            Description = "Test Group 1 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 2,
            Name = "Group 2",
            Description = "Test Group 2 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 9,
            Name = "Group 3",
            Description = "Test Group 3 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Adding Locations
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "Location 1",
            Code = "Code-1",
            WarehouseId = 101
        });
        Context.Locations.Add(new Location
        {
            Id = 9,
            Name = "Location 99",
            Code = "Code-3",
            WarehouseId = 103
        });

        // Adding Clients
        Context.Clients.Add(new Client
        {
            Id = 9,
            Name = "Client 1",
            Address = "123 Main Street",
            City = "City 1",
            ZipCode = "12345",
            Province = "Province 1",
            Country = "Country 1",
            ContactName = "John Doe",
            ContactPhone = "+1234567890",
            ContactEmail = "johndoe@example.com",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges(); // heel raar hoe dit werkt ik kan dit niet op de constructor 
                               //zetten zonder dat dingen breken op een niet logische volgorde



        var CheckServiceitemGroup = new CrudService<ItemGroup>(Context);
        var CheckServiceitemLocation = new CrudService<Location>(Context);

        ItemGroup itemGroup = CheckServiceitemGroup.Get(9);
        Location location = CheckServiceitemLocation.Get(9);

        Assert.Equal("Group 3", itemGroup.Name);
        Assert.Equal("Location 99", location.Name);
    }
    [Fact]
    public void AnotherGetTestWithListInClass()
    {
        // Given
        var testOrder = new Order
        {
            Id = 1,
            SourceId = 1001,
            OrderDate = DateTime.UtcNow,
            RequestDate = DateTime.UtcNow.AddDays(7),
            Reference = "REF12345",
            ReferenceExtra = "Extra details",
            OrderStatus = "Pending",
            Notes = "This is a test order",
            ShippingNotes = "Handle with care",
            PickingNote = "Prioritize fragile items",
            WarehouseId = 2,
            ShipTo = 10,
            BillTo = 20,
            ShipmentId = 30,
            TotalAmount = 500.00m,
            TotalDiscount = 50.00m,
            TotalTax = 25.00m,
            TotalSurcharge = 10.00m,
            CreatedAt = DateTime.UtcNow.ToString(),
            UpdatedAt = DateTime.UtcNow.ToString(),
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 1,
                    OrderItemId = "101",
                    OrderId = 1,
                    Amount = 5
                },
                new OrderItem
                {
                    Id = 2,
                    OrderItemId = "102",
                    OrderId = 1,
                    Amount = 3
                }
            }
        };
        Context.Orders.Add(testOrder);
        Context.SaveChanges();
        var OrderService = new CrudService<Order>(Context);

        // When
        var checkOrderItem = OrderService.Get(1);

        string TheID = OrderService.Get(1).Items[0].OrderItemId;

        // Then
        Assert.Equal("101", TheID);
    }
    [Fact]
    public void GetAllTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 2,
            Name = "Group 2",
            Description = "Test Group 2 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 9,
            Name = "Group 3",
            Description = "Test Group 3 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Adding Locations
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "Location 1",
            Code = "Code-1",
            WarehouseId = 101
        });
        Context.Locations.Add(new Location
        {
            Id = 9,
            Name = "Location 99",
            Code = "Code-3",
            WarehouseId = 103
        });
        Context.SaveChanges();
        // When
        var CheckServiceitemGroup = new CrudService<ItemGroup>(Context);
        var CheckServiceitemLocation = new CrudService<Location>(Context);



        // Then
        List<ItemGroup> itemGroups = CheckServiceitemGroup.GetAll();
        List<Location> locations = CheckServiceitemLocation.GetAll();

        Assert.Equal(2, itemGroups.Count);
        Assert.Equal(2, locations.Count);
    }
    [Fact]
    public void DeleteTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "Group 1",
            Description = "Test Group 1 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 2,
            Name = "Group 2",
            Description = "Test Group 2 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 9,
            Name = "Group 3",
            Description = "Test Group 3 Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Adding Locations
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "Location 1",
            Code = "Code-1",
            WarehouseId = 101
        });
        Context.Locations.Add(new Location
        {
            Id = 9,
            Name = "Location 99",
            Code = "Code-3",
            WarehouseId = 103
        });
        Context.SaveChanges();

        // When
        var CheckServiceitemGroup = new CrudService<ItemGroup>(Context);
        var CheckServiceitemLocation = new CrudService<Location>(Context);

        bool check = CheckServiceitemGroup.Delete(1);
        bool check2 = CheckServiceitemLocation.Delete(1);
        // Then
        Assert.Equal(2, Context.ItemGroups.ToList().Count);
        Assert.Equal(1, Context.Locations.ToList().Count);
        Assert.True(check);
        Assert.True(check2);
    }
    [Fact]
    public void Update() // can be both put and/or patch 
    {
        // Given
        var supplierService = new CrudService<Supplier>(Context);
        Context.Suppliers.Add(new Supplier
        {
            Id = 1,
            Code = "baka",
            Name = "Supplier B",
            Address = "321 Elm Street",
            AddressExtra = "Apt 9",
            City = "Brooklyn",
            ZipCode = "11201",
            Province = "NY",
            Country = "USA",
            ContactName = "Jane Doe",
            PhoneNumber = "+987654321",
            Reference = "REF002",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });
        Context.SaveChanges();

        // When
        supplierService.Put(new Supplier
        {
            Id = 1,
            Code = "sigaar",
            Name = "Supplier B",
            Address = "321 Elm Street",
            AddressExtra = "Apt 9",
            City = "Brooklyn",
            ZipCode = "11201",
            Province = "NY",
            Country = "USA",
            ContactName = "Jane Doe",
            PhoneNumber = "+987654321",
            Reference = "REF002",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        Supplier check = supplierService.Get(1);
        Assert.Equal("sigaar", check.Code);
    }
    [Fact]
    public void TestingIfTimeGetsUpdated()
    {
        // Given
        var now = DateTime.UtcNow;
        Context.Inventories.Add(new Inventory
        {
            Id = 1,
            ItemId = "ITEM001",
            Description = "jewbaka",
            ItemReference = "REF123",
            TotalOnHand = 100,
            TotalExpected = 200,
            TotalOrdered = 150,
            TotalAllocated = 50,
            TotalAvailable = 50,
            Locations = new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Code = "LOC001",
                    Name = "Warehouse 1"
                },
                new Location
                {
                    Id = 2,
                    Code = "LOC002",
                    Name = "Warehouse 2"
                }
            }
        });
        Context.SaveChanges();


        // When
        var InventoryService = new CrudService<Inventory>(Context);
        InventoryService.Put(new Inventory
        {
            Id = 1,
            ItemId = "ITEM001",
            Description = "ReDone",
            ItemReference = "REF123",
            TotalOnHand = 100,
            TotalExpected = 200,
            TotalOrdered = 150,
            TotalAllocated = 50,
            TotalAvailable = 50,
            Locations = new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Code = "LOC001",
                    Name = "Warehouse 1"
                },
                new Location
                {
                    Id = 2,
                    Code = "LOC002",
                    Name = "Warehouse 2"
                }
            }
        });

        var UpdatedInventory = InventoryService.Get(1);

        // Then
        Console.WriteLine($"Now: {now}");
        Console.WriteLine($"UpdatedAt: {DateTime.Parse(UpdatedInventory.UpdatedAt)}");

        Assert.True(DateTime.Parse(UpdatedInventory.UpdatedAt) >= now);
        System.Console.WriteLine($"Test 2{DateTime.Parse(UpdatedInventory.UpdatedAt) >= now}");

        Assert.True((now - DateTime.Parse(UpdatedInventory.UpdatedAt)).Milliseconds < 500);
        System.Console.WriteLine($"Test 2{(now - DateTime.Parse(UpdatedInventory.UpdatedAt)).Milliseconds < 500}");
    }

}
// alles word gerunt voordat de check word gedaan of iets goed is of niet