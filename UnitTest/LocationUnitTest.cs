using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class LocationUnitTest
{
    private ModelContext Context;

    public LocationUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetLocationTest()
    {
        // Given
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "location 1",
            WarehouseId = 2,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var locationService = new CrudService<Location>(Context);

        // When
        Location location = locationService.Get(1);

        // Then
        Assert.Equal("location 1", location.Name);
    }

    [Fact]
    public void GetAllLocationTest()
    {
        // Given
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "location 1",
            WarehouseId = 2,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.Locations.Add(new Location
        {
            Id = 2,
            Name = "location 2",
            WarehouseId = 1,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var locationService = new CrudService<Location>(Context);

        // When
        List<Location> locations = locationService.GetAll();

        // Then
        Assert.Equal(2, locations.Count);
    }

    [Fact]
    public void DeleteLocationTest()
    {
        // Given
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "location 1",
            WarehouseId = 2,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var locationService = new CrudService<Location>(Context);

        // When
        bool result = locationService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(locationService.Get(1));
    }

    [Fact]
    public void UpdateLocationTest()
    {
        // Given
        Context.Locations.Add(new Location
        {
            Id = 1,
            Name = "location 1",
            WarehouseId = 2,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var locationService = new CrudService<Location>(Context);

        // When
        locationService.Put(new Location
        {
            Id = 1,
            Name = "Updated Location",
            WarehouseId = 3,
            Code = "A.7.0",

            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        Location? updatedLocation = locationService.Get(1);
        Assert.Equal("Updated Location", updatedLocation.Name);
    }
   
}
