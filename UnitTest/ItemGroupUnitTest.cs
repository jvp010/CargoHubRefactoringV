using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ItemGroupUnitTest
{
    private ModelContext Context;

    public ItemGroupUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetItemGroupTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "ItemGroup 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemGroupService = new CrudService<ItemGroup>(Context);

        // When
        ItemGroup itemGroup = itemGroupService.Get(1);

        // Then
        Assert.Equal("ItemGroup 1", itemGroup.Name);
    }

    [Fact]
    public void GetAllItemGroupsTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "ItemGroup 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 2,
            Name = "ItemGroup 2",
            Description = "Description 2",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemGroupService = new CrudService<ItemGroup>(Context);

        // When
        List<ItemGroup> itemGroups = itemGroupService.GetAll();

        // Then
        Assert.Equal(2, itemGroups.Count);
    }

    [Fact]
    public void DeleteItemGroupTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "ItemGroup 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemGroupService = new CrudService<ItemGroup>(Context);

        // When
        bool result = itemGroupService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(itemGroupService.Get(1));
    }

    [Fact]
    public void UpdateItemGroupTest()
    {
        // Given
        Context.ItemGroups.Add(new ItemGroup
        {
            Id = 1,
            Name = "ItemGroup 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemGroupService = new CrudService<ItemGroup>(Context);

        // When
        itemGroupService.Put(new ItemGroup
        {
            Id = 1,
            Name = "Updated ItemGroup",
            Description = "Updated Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        ItemGroup updatedItemGroup = itemGroupService.Get(1);
        Assert.Equal("Updated ItemGroup", updatedItemGroup.Name);
    }
}