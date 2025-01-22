using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class WarehouseUnitTest
{
    private ModelContext Context;

    public WarehouseUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetWarehouseTest()
    {
        // Given
        Context.Warehouses.Add(new Warehouse
        {
            Id = 1,
            Code = "WH001",
            Name = "Warehouse 1",
            Address = "123 Warehouse Street",
            City = "Warehouse City",
            Zip = "12345", 
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var warehouseService = new CrudService<Warehouse>(Context);

        // When
        Warehouse warehouse = warehouseService.Get(1);

        // Then
        Assert.Equal("Warehouse 1", warehouse.Name);
    }

    [Fact]
    public void GetAllWarehousesTest()
    {
        // Given
        Context.Warehouses.Add(new Warehouse
        {
            Id = 1,
            Code = "WH001", 
            Name = "Warehouse 1",
            Address = "123 Warehouse Street",
            City = "Warehouse City",
            Zip = "12345",
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.Warehouses.Add(new Warehouse
        {
            Id = 2,
            Code = "WH002",
            Name = "Warehouse 2",
            Address = "456 Warehouse Avenue",
            City = "Warehouse City",
            Zip = "67890", 
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var warehouseService = new CrudService<Warehouse>(Context);

        // When
        List<Warehouse> warehouses = warehouseService.GetAll();

        // Then
        Assert.Equal(2, warehouses.Count);
    }

    [Fact]
    public void DeleteWarehouseTest()
    {
        // Given
        Context.Warehouses.Add(new Warehouse
        {
            Id = 1,
            Code = "WH001", 
            Name = "Warehouse 1",
            Address = "123 Warehouse Street",
            City = "Warehouse City",
            Zip = "12345", 
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var warehouseService = new CrudService<Warehouse>(Context);

        // When
        bool result = warehouseService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(warehouseService.Get(1));
    }

    [Fact]
    public void UpdateWarehouseTest()
    {
        // Given
        Context.Warehouses.Add(new Warehouse
        {
            Id = 1,
            Code = "WH001", 
            Name = "Warehouse 1",
            Address = "123 Warehouse Street",
            City = "Warehouse City",
            Zip = "12345", 
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var warehouseService = new CrudService<Warehouse>(Context);

        // When
        warehouseService.Put(new Warehouse
        {
            Id = 1,
            Code = "WH001-UPDATED", 
            Name = "Updated Warehouse",
            Address = "123 Warehouse Street",
            City = "Warehouse City",
            Zip = "12345", 
            Province = "Warehouse Province",
            Country = "Warehouse Country",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        Warehouse updatedWarehouse = warehouseService.Get(1);
        Assert.Equal("Updated Warehouse", updatedWarehouse.Name);
    }
}
