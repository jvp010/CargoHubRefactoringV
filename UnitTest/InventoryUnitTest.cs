using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryUnitTest
{
    private ModelContext Context;

    public InventoryUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetInventoryTest()
    {
        // Given
        Context.Inventories.Add(new Inventory
        {
            Id = 1,
            ItemId = "1",
            Description = "Test Inventory 1",
            ItemReference = "Test Reference 1",
            Locations = new List<int> { 1, 2 },
            TotalOnHand = 10,
            TotalExpected = 10,
            TotalOrdered = 10,
            TotalAllocated = 10,
            TotalAvailable = 10
        });
        Context.SaveChanges();

        var inventoryService = new InventoryService(Context);

        // When
        Inventory inventory = inventoryService.Get(1);

        // Then
        Assert.Equal("Test Inventory 1", inventory.Description);
    }

    [Fact]
    public void GetAllInventoriesTest()
    {
        // Given
        Context.Inventories.Add(new Inventory
        {
            Id = 1,
            ItemId = "1",
            Description = "Test Inventory 1",
            ItemReference = "Test Reference 1",
            Locations = new List<int> { 1, 2 },
            TotalOnHand = 10,
            TotalExpected = 10,
            TotalOrdered = 10,
            TotalAllocated = 10,
            TotalAvailable = 10
        });
        Context.Inventories.Add(new Inventory
        {
            Id = 2,
            ItemId = "2",
            Description = "Test Inventory 2",
            ItemReference = "Test Reference 2",
            Locations = new List<int> { 3, 4 },
            TotalOnHand = 20,
            TotalExpected = 20,
            TotalOrdered = 20,
            TotalAllocated = 20,
            TotalAvailable = 20
        });
        Context.SaveChanges();

        var inventoryService = new InventoryService(Context);

        // When
        List<Inventory> inventories = inventoryService.GetAll();

        // Then
        Assert.Equal(2, inventories.Count);
    }

    [Fact]
    public void CreateInventoryTest()
    {
        // Given
        var newInventory = new Inventory
        {
            ItemId = "3",
            Description = "New Inventory",
            ItemReference = "New Reference",
            Locations = new List<int>(),
            TotalOnHand = 30,
            TotalExpected = 30,
            TotalOrdered = 30,
            TotalAllocated = 30,
            TotalAvailable = 30
        };
        var inventoryService = new InventoryService(Context);

        // When
        inventoryService.Post(newInventory);
        Context.SaveChanges();

        // Then
        Inventory inventory = inventoryService.Get(newInventory.Id);
        Assert.Equal("New Inventory", inventory.Description);
    }

    [Fact]
    public void UpdateInventoryTest()
    {
        // Given
        Context.Inventories.Add(new Inventory
        {
            Id = 1,
            ItemId = "1",
            Description = "Old Inventory",
            ItemReference = "Old Reference",
            Locations = new List<int>(),
            TotalOnHand = 10,
            TotalExpected = 10,
            TotalOrdered = 10,
            TotalAllocated = 10,
            TotalAvailable = 10
        });
        Context.SaveChanges();

        var inventoryService = new InventoryService(Context);

        // When
        var updatedInventory = new Inventory
        {
            Id = 1,
            ItemId = "1",
            Description = "Updated Inventory",
            ItemReference = "Updated Reference",
            Locations = new List<int>(),
            TotalOnHand = 15,
            TotalExpected = 15,
            TotalOrdered = 15,
            TotalAllocated = 15,
            TotalAvailable = 15
        };
        inventoryService.Put(updatedInventory);
        Context.SaveChanges();

        // Then
        Inventory inventory = inventoryService.Get(1);
        Assert.Equal("Updated Inventory", inventory.Description);
    }

    [Fact]
    public void DeleteInventoryTest()
    {
        // Given
        Context.Inventories.Add(new Inventory
        {
            Id = 1,
            ItemId = "1",
            Description = "Test Inventory",
            ItemReference = "Test Reference",
            Locations = new List<int> { 1, 2 },
            TotalOnHand = 10,
            TotalExpected = 10,
            TotalOrdered = 10,
            TotalAllocated = 10,
            TotalAvailable = 10
        });
        Context.SaveChanges();

        var inventoryService = new InventoryService(Context);

        // When
        bool result = inventoryService.Delete(1);
        Context.SaveChanges();

        // Then
        Assert.True(result);
        Assert.Null(inventoryService.Get(1));
    }
}
