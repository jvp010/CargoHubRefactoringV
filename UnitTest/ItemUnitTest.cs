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





    [Fact]
    public void GetItemsForItemGroupTest()
    {
        // Given
        var itemGroup = new ItemGroup
        {
            Id = 5,
            Name = "Stationery",
            Description = "Group for all stationery items",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.ItemGroups.Add(itemGroup);
        //{'CommodityCode', 'ModelNumber', 'UpcCode'}
        var item1 = new Item
        {

            Uid = "P000005",
            Code = "mHo61152n",
            Description = "Stand-alone 24hour emulation",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "there",
            ItemGroup = 5,
            ItemType = 28,
            UnitPurchaseQuantity = 44,
            UnitOrderQuantity = 2,
            PackOrderQuantity = 20,
            SupplierId = 35,
            SupplierCode = "SUP347",
            SupplierPartNumber = "NzG-36a1",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item1);


        var item2 = new Item
        {
            Uid = "P000006",
            Code = "aBc1234",
            Description = "Another item",

            ShortDescription = "sample",
            ItemGroup = 5,
            ItemType = 28,
            UnitPurchaseQuantity = 10,
            UnitOrderQuantity = 5,
            PackOrderQuantity = 50,
            SupplierId = 35,
            SupplierCode = "SUP348",
            SupplierPartNumber = "NzG-36a2",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item2);
        Context.SaveChanges();


        // When
        var itemService = new ItemService(Context);
        List<Item> items = itemService.GetItemsForItemGroup(5);


        // Then
        Assert.Equal(2, items.Count);
    }


    [Fact]
    public void GetItemsForItemLineTest()
    {
        // Given
        var ItemLine = new ItemLine
        {
            Id = 5,
            Name = "Stationery",
            Description = "Group for all stationery items",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.ItemLines.Add(ItemLine);
        //{'CommodityCode', 'ModelNumber', 'UpcCode'}
        var item1 = new Item
        {

            Uid = "P000005",
            Code = "mHo61152n",
            Description = "Stand-alone 24hour emulation",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "there",
            ItemLine = 5,
            ItemGroup = 35,
            ItemType = 28,
            UnitPurchaseQuantity = 44,
            UnitOrderQuantity = 2,
            PackOrderQuantity = 20,
            SupplierId = 35,
            SupplierCode = "SUP347",
            SupplierPartNumber = "NzG-36a1",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item1);


        var item2 = new Item
        {
            Uid = "P000006",
            Code = "aBc1234",
            Description = "Another item",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "sample",
            ItemLine = 5,
            ItemGroup = 35,
            ItemType = 28,
            UnitPurchaseQuantity = 10,
            UnitOrderQuantity = 5,
            PackOrderQuantity = 50,
            SupplierId = 35,
            SupplierCode = "SUP348",
            SupplierPartNumber = "NzG-36a2",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item2);
        Context.SaveChanges();


        // When
        var itemService = new ItemService(Context);
        List<Item> items = itemService.GetItemsForItemLine(5);


        // Then
        Assert.Equal(2, items.Count);
    }
    [Fact]
    public void GetItemsForItemTypeTest()
    {
        // Given
        var ItemType = new ItemType
        {
            Id = 5,
            Name = "Stationery",
            Description = "Group for all stationery items",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.ItemTypes.Add(ItemType);
        //{'CommodityCode', 'ModelNumber', 'UpcCode'}
        var item1 = new Item
        {

            Uid = "P000005",
            Code = "mHo61152n",
            Description = "Stand-alone 24hour emulation",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "there",
            ItemLine = 5,
            ItemGroup = 35,
            ItemType = 5,
            UnitPurchaseQuantity = 44,
            UnitOrderQuantity = 2,
            PackOrderQuantity = 20,
            SupplierId = 35,
            SupplierCode = "SUP347",
            SupplierPartNumber = "NzG-36a1",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item1);
        var item2 = new Item
        {
            Uid = "P000006",
            Code = "aBc1234",
            Description = "Another item",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "sample",
            ItemLine = 53,
            ItemGroup = 5,
            ItemType = 28,
            UnitPurchaseQuantity = 10,
            UnitOrderQuantity = 5,
            PackOrderQuantity = 50,
            SupplierId = 35,
            SupplierCode = "SUP348",
            SupplierPartNumber = "NzG-36a2",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item2);
        Context.SaveChanges();

        var itemService = new ItemService(Context);
        List<Item> items = itemService.GetItemsForItemGroup(5);


        // Then
        Assert.Equal(1, items.Count);
    }


    [Fact]
    public void GetItemsForSupplier()
    {
         // Given
        var supplier = new Supplier
        {
            Id = 1,
            Code = "SUP0001",
            Name = "Lee, Parks and Johnson",
            Address = "5989 Sullivan Drives",
            AddressExtra = "Apt. 996",
            City = "Port Anitaburgh",
            ZipCode = "91688",
            Province = "Illinois",
            Country = "Czech Republic",
            ContactName = "Toni Barnett",
            PhoneNumber = "363.541.7282x36825",
            Reference = "LPaJ-SUP0001",
            CreatedAt = DateTime.Parse("1971-10-20 18:06:17").ToString(),
            UpdatedAt = DateTime.Parse("1985-06-08 00:13:46").ToString()
        };

        Context.Suppliers.Add(supplier);
        var item1 = new Item
        {

            Uid = "P000005",
            Code = "mHo61152n",
            Description = "Stand-alone 24hour emulation",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "there",
            ItemLine = 5,
            ItemGroup = 35,
            ItemType = 5,
            UnitPurchaseQuantity = 44,
            UnitOrderQuantity = 2,
            PackOrderQuantity = 20,
            SupplierId = 1,
            SupplierCode = "SUP347",
            SupplierPartNumber = "NzG-36a1",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item1);
        var item2 = new Item
        {
            Uid = "P000006",
            Code = "aBc1234",
            Description = "Another item",
            CommodityCode = "1",
            ModelNumber = "test",
            UpcCode = "3",
            ShortDescription = "sample",
            ItemLine = 53,
            ItemGroup = 5,
            ItemType = 28,
            UnitPurchaseQuantity = 10,
            UnitOrderQuantity = 5,
            PackOrderQuantity = 50,
            SupplierId = 2,
            SupplierCode = "SUP348",
            SupplierPartNumber = "NzG-36a2",
            CreatedAt = DateTime.Parse("2017-04-01 09:15:22").ToString(),
            UpdatedAt = DateTime.Parse("2024-06-10 14:45:07").ToString()
        };
        Context.Items.Add(item2);
        Context.SaveChanges();

        var itemService = new ItemService(Context);
        List<Item> items = itemService.GetItemsForSupplier(1);


        // Then
        Assert.Equal(1, items.Count);
    }
}
