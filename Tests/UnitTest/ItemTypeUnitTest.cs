using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ItemTypeUnitTest
{
    private ModelContext Context;

    public ItemTypeUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetItemTypeTest()
    {
        // Given
        Context.ItemTypes.Add(new ItemType
        {
            Id = 1,
            Name = "ItemType 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemTypeService = new CrudService<ItemType>(Context);

        // When
        ItemType itemType = itemTypeService.Get(1);

        // Then
        Assert.Equal("ItemType 1", itemType.Name);
    }

    [Fact]
    public void GetAllItemTypesTest()
    {
        // Given
        Context.ItemTypes.Add(new ItemType
        {
            Id = 1,
            Name = "ItemType 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemTypes.Add(new ItemType
        {
            Id = 2,
            Name = "ItemType 2",
            Description = "Description 2",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemTypeService = new CrudService<ItemType>(Context);

        // When
        List<ItemType> itemTypes = itemTypeService.GetAll();

        // Then
        Assert.Equal(2, itemTypes.Count);
    }

    [Fact]
    public void DeleteItemTypeTest()
    {
        // Given
        Context.ItemTypes.Add(new ItemType
        {
            Id = 1,
            Name = "ItemType 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemTypeService = new CrudService<ItemType>(Context);

        // When
        bool result = itemTypeService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(itemTypeService.Get(1));
    }

    [Fact]
    public void UpdateItemTypeTest()
    {
        // Given
        Context.ItemTypes.Add(new ItemType
        {
            Id = 1,
            Name = "ItemType 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemTypeService = new CrudService<ItemType>(Context);

        // When
        itemTypeService.Put(new ItemType
        {
            Id = 1,
            Name = "Updated ItemType",
            Description = "Updated Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        ItemType updatedItemType = itemTypeService.Get(1);
        Assert.Equal("Updated ItemType", updatedItemType.Name);
    }
}