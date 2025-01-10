using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class SuppliersUnitTest
{
    private ModelContext Context;

    public SuppliersUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetSupplierTest()
    {
        // Given
        Context.Suppliers.Add(new Supplier
        {
            Id = 1,
            Code = "SUP001",
            Name = "Supplier 1",
            Address = "123 Supplier Street",
            City = "Supplier City",
            ZipCode = "12345",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact",
            PhoneNumber = "+1234567890",
            Reference = "REF001",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var supplierService = new CrudService<Supplier>(Context);

        // When
        Supplier supplier = supplierService.Get(1);

        // Then
        Assert.Equal("Supplier 1", supplier.Name);
    }

    [Fact]
    public void GetAllSuppliersTest()
    {
        // Given
        Context.Suppliers.Add(new Supplier
        {
            Id = 1,
            Code = "SUP001",
            Name = "Supplier 1",
            Address = "123 Supplier Street",
            City = "Supplier City",
            ZipCode = "12345",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact",
            PhoneNumber = "+1234567890",
            Reference = "REF001",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.Suppliers.Add(new Supplier
        {
            Id = 2,
            Code = "SUP002",
            Name = "Supplier 2",
            Address = "456 Supplier Avenue",
            City = "Supplier City",
            ZipCode = "67890",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact 2",
            PhoneNumber = "+0987654321",
            Reference = "REF002",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var supplierService = new CrudService<Supplier>(Context);

        // When
        List<Supplier> suppliers = supplierService.GetAll();

        // Then
        Assert.Equal(2, suppliers.Count);
    }

    [Fact]
    public void DeleteSupplierTest()
    {
        // Given
        Context.Suppliers.Add(new Supplier
        {
            Id = 1,
            Code = "SUP001",
            Name = "Supplier 1",
            Address = "123 Supplier Street",
            City = "Supplier City",
            ZipCode = "12345",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact",
            PhoneNumber = "+1234567890",
            Reference = "REF001",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var supplierService = new CrudService<Supplier>(Context);

        // When
        bool result = supplierService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(supplierService.Get(1));
    }

    [Fact]
    public void UpdateSupplierTest()
    {
        // Given
        Context.Suppliers.Add(new Supplier
        {
            Id = 1,
            Code = "SUP001",
            Name = "Supplier 1",
            Address = "123 Supplier Street",
            City = "Supplier City",
            ZipCode = "12345",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact",
            PhoneNumber = "+1234567890",
            Reference = "REF001",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var supplierService = new CrudService<Supplier>(Context);

        // When
        supplierService.Put(new Supplier
        {
            Id = 1,
            Code = "SUP001-UPDATED",
            Name = "Updated Supplier",
            Address = "123 Supplier Street",
            City = "Supplier City",
            ZipCode = "12345",
            Province = "Supplier Province",
            Country = "Supplier Country",
            ContactName = "Supplier Contact",
            PhoneNumber = "+1234567890",
            Reference = "REF001",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Then
        Supplier updatedSupplier = supplierService.Get(1);
        Assert.Equal("SUP001-UPDATED", updatedSupplier.Code);
    }
}
