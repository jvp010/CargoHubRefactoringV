using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class ItemLineUnitTest
{
    private ModelContext Context;

    public ItemLineUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetItemLineTest()
    {
        // Given
        Context.ItemLines.Add(new ItemLine
        {
            Id = 1,
            Name = "ItemLine 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemLineService = new CrudService<ItemLine>(Context);

        // When
        ItemLine itemLine = itemLineService.Get(1);

        // Then
        Assert.Equal("ItemLine 1", itemLine.Name);
    }

    [Fact]
    public void GetAllItemLinesTest()
    {
        // Given
        Context.ItemLines.Add(new ItemLine
        {
            Id = 1,
            Name = "ItemLine 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.ItemLines.Add(new ItemLine
        {
            Id = 2,
            Name = "ItemLine 2",
            Description = "Description 2",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemLineService = new CrudService<ItemLine>(Context);

        // When
        List<ItemLine> itemLines = itemLineService.GetAll();

        // Then
        Assert.Equal(2, itemLines.Count);
    }

    [Fact]
    public void DeleteItemLineTest()
    {
        // Given
        Context.ItemLines.Add(new ItemLine
        {
            Id = 1,
            Name = "ItemLine 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemLineService = new CrudService<ItemLine>(Context);

        // When
        bool result = itemLineService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(itemLineService.Get(1));
    }

    [Fact]
    public void UpdateItemLineTest()
    {
        // Given
        Context.ItemLines.Add(new ItemLine
        {
            Id = 1,
            Name = "ItemLine 1",
            Description = "Description 1",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var itemLineService = new CrudService<ItemLine>(Context);

        // When
        itemLineService.Put(new ItemLine
        {
            Id = 1,
            Name = "Updated ItemLine",
            Description = "Updated Description",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        ItemLine updatedItemLine = itemLineService.Get(1);
        Assert.Equal("Updated ItemLine", updatedItemLine.Name);
    }
}