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


public class ClientUnitTest
{
    private ModelContext Context;

    public ClientUnitTest()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        Context = new ModelContext(options);
    }

    [Fact]
    public void GetClientTest()
    {
        // Given
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

        Context.SaveChanges();

        var clientService = new CrudService<Client>(Context);

        // When
        Client client = clientService.Get(9);

        // Then
        Assert.Equal("Client 1", client.Name);
    }

    [Fact]
    public void GetAllClientsTest()
    {
        // Given
        Context.Clients.Add(new Client
        {
            Id = 1,
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

        Context.Clients.Add(new Client
        {
            Id = 2,
            Name = "Client 2",
            Address = "456 Elm Street",
            City = "City 2",
            ZipCode = "67890",
            Province = "Province 2",
            Country = "Country 2",
            ContactName = "Jane Doe",
            ContactPhone = "+0987654321",
            ContactEmail = "janedoe@example.com",
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        Context.SaveChanges();

        var clientService = new CrudService<Client>(Context);

        // When
        List<Client> clients = clientService.GetAll();

        // Then
        Assert.Equal(2, clients.Count);
    }

    [Fact]
    public void DeleteClientTest()
    {
        // Given
        Context.Clients.Add(new Client
        {
            Id = 1,
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

        Context.SaveChanges();

        var clientService = new CrudService<Client>(Context);

        // When
        bool result = clientService.Delete(1);

        // Then
        Assert.True(result);
        Assert.Null(clientService.Get(1));
    }

    [Fact]
    public void UpdateClientTest()
    {
        // Given
        Context.Clients.Add(new Client
        {
            Id = 1,
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

        Context.SaveChanges();

        var clientService = new CrudService<Client>(Context);

        // When
        clientService.Put(new Client
        {
            Id = 1,
            Name = "Updated Client",
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

        // Then
        Client updatedClient = clientService.Get(1);
        Assert.Equal("Updated Client", updatedClient.Name);
    }
}
