using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ItemUnitTest
{
    private ModelContext Context;

    public ItemUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetItemTest()
    {
        // Given
        Context.Items.Add(new Item
        {
            Uid = "P000001",
            Code = "sjQ23408K",
            Description = "Face-to-face clear-thinking complexity",
            ShortDescription = "must",
            UpcCode = "6523540947122",
            ModelNumber = "63-OFFTq0T",
            CommodityCode = "oTo304",
            ItemLine = 11,
            ItemGroup = 73,
            ItemType = 14,
            UnitPurchaseQuantity = 47,
            UnitOrderQuantity = 13,
            PackOrderQuantity = 11,
            SupplierId = 34,
            SupplierCode = "SUP423",
            SupplierPartNumber = "E-86805-uTM",
            CreatedAt = "2015-02-19 16:08:24",
            UpdatedAt = "2015-09-26 06:37:56"
        });

        Context.SaveChanges();

        var itemService = new ItemService(Context);

        // When
        Item? item = itemService.Get("P000001");

        // Then
        Assert.Equal("sjQ23408K", item.Code);
        Assert.Equal(11, item.ItemLine);
    }

    [Fact]
    public void GetAllItemsTest()
    {
        // Given
        Context.Items.Add(new Item
        {
            Uid = "P000001",
            Code = "sjQ23408K",
            Description = "Face-to-face clear-thinking complexity",
            ShortDescription = "must",
            UpcCode = "6523540947122",
            ModelNumber = "63-OFFTq0T",
            CommodityCode = "oTo304",
            ItemLine = 11,
            ItemGroup = 73,
            ItemType = 14,
            UnitPurchaseQuantity = 47,
            UnitOrderQuantity = 13,
            PackOrderQuantity = 11,
            SupplierId = 34,
            SupplierCode = "SUP423",
            SupplierPartNumber = "E-86805-uTM",
            CreatedAt = "2015-02-19 16:08:24",
            UpdatedAt = "2015-09-26 06:37:56"
        });

        Context.Items.Add(new Item
        {
            Uid = "P000002",
            Code = "abcd1234",
            Description = "Sample item",
            ShortDescription = "short",
            UpcCode = "1234567890123",
            ModelNumber = "MOD123",
            CommodityCode = "C001",
            ItemLine = 12,
            ItemGroup = 74,
            ItemType = 15,
            UnitPurchaseQuantity = 50,
            UnitOrderQuantity = 20,
            PackOrderQuantity = 15,
            SupplierId = 35,
            SupplierCode = "SUP424",
            SupplierPartNumber = "E-86806-uTM",
            CreatedAt = "2016-02-19 16:08:24",
            UpdatedAt = "2016-09-26 06:37:56"
        });

        Context.SaveChanges();

        var itemService = new ItemService(Context);

        // When
        List<Item> items = itemService.GetAll();

        // Then
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void DeleteItemTest()
    {
        // Given
        Context.Items.Add(new Item
        {
            Uid = "P000001",
            Code = "sjQ23408K",
            Description = "Face-to-face clear-thinking complexity",
            ShortDescription = "must",
            UpcCode = "6523540947122",
            ModelNumber = "63-OFFTq0T",
            CommodityCode = "oTo304",
            ItemLine = 11,
            ItemGroup = 73,
            ItemType = 14,
            UnitPurchaseQuantity = 47,
            UnitOrderQuantity = 13,
            PackOrderQuantity = 11,
            SupplierId = 34,
            SupplierCode = "SUP423",
            SupplierPartNumber = "E-86805-uTM",
            CreatedAt = "2015-02-19 16:08:24",
            UpdatedAt = "2015-09-26 06:37:56"
        });

        Context.SaveChanges();

        var itemService = new ItemService(Context);

        // When
        bool result = itemService.Delete("P000001");

        // Then
        Assert.True(result);
        Assert.Null(itemService.Get("P000001"));
    }

    [Fact]
    public void UpdateItemTest()
    {
        // Given
        Context.Items.Add(new Item
        {
            Uid = "P000001",
            Code = "sjQ23408K",
            Description = "Face-to-face clear-thinking complexity",
            ShortDescription = "must",
            UpcCode = "6523540947122",
            ModelNumber = "63-OFFTq0T",
            CommodityCode = "oTo304",
            ItemLine = 11,
            ItemGroup = 73,
            ItemType = 14,
            UnitPurchaseQuantity = 47,
            UnitOrderQuantity = 13,
            PackOrderQuantity = 11,
            SupplierId = 34,
            SupplierCode = "SUP423",
            SupplierPartNumber = "E-86805-uTM",
            CreatedAt = "2015-02-19 16:08:24",
            UpdatedAt = "2015-09-26 06:37:56"
        });

        Context.SaveChanges();

        var itemService = new ItemService(Context);

        // When
        itemService.Put(new Item
        {
            Uid = "P000001",
            Code = "UpdatedCode",
            Description = "Updated Description",
            ShortDescription = "updated short",
            UpcCode = "9876543210987",
            ModelNumber = "MOD456",
            CommodityCode = "C002",
            ItemLine = 15,
            ItemGroup = 76,
            ItemType = 18,
            UnitPurchaseQuantity = 60,
            UnitOrderQuantity = 25,
            PackOrderQuantity = 20,
            SupplierId = 36,
            SupplierCode = "SUP425",
            SupplierPartNumber = "E-86807-uTM",
            CreatedAt = "2015-02-19 16:08:24",
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        Item updatedItem = itemService.Get("P000001");
        Assert.Equal("UpdatedCode", updatedItem.Code);
    }
}
