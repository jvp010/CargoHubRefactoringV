using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClientControllerIntegrationTests
{
    private readonly ModelContext _context;
    private readonly GenericController<Client> _controller;

    public ClientControllerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Ensure unique DB instance
            .Options;

        _context = new ModelContext(options);
        var crudService = new CrudService<Client>(_context);
        _controller = new GenericController<Client>(crudService, _context);
    }

    [Fact]
    public async Task GetClient_ReturnsOk_WhenClientExists()
    {
        // Arrange
        var client = new Client
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
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetbyId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedClient = Assert.IsType<Client>(okResult.Value);
        Assert.Equal("Client 1", returnedClient.Name);
    }

    [Fact]
    public async Task GetAllClients_ReturnsOk_WhenClientsExist()
    {
        // Arrange
        var clients = new List<Client>
        {
            new Client 
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
            },
            new Client 
            { 
                Id = 2, 
                Name = "Client 2", 
                Address = "456 Elm Street", 
                City = "City 2", 
                ZipCode = "54321", 
                Province = "Province 2", 
                Country = "Country 2", 
                ContactName = "Jane Doe", 
                ContactPhone = "+0987654321", 
                ContactEmail = "janedoe@example.com", 
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
            }
        };

        _context.Clients.AddRange(clients);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedClients = Assert.IsType<List<Client>>(okResult.Value);
        Assert.Equal(2, returnedClients.Count);
    }

    [Fact]
    public async Task DeleteClient_ReturnsOk_WhenClientExists()
    {
        // Arrange
        var client = new Client
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
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("id " + 1 + " has been deleted", okResult.Value);
        Assert.Null(await _context.Clients.FindAsync(1));
    }

    [Fact]
    public async Task UpdateClient_ReturnsOk_WhenClientIsUpdated()
    {
        // Arrange
        var client = new Client
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
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var updatedClient = new Client
        {
            Id = 1,
            Name = "Updated Client",
            Address = "123 Main Street",
            City = "Updated City",
            ZipCode = "54321",
            Province = "Updated Province",
            Country = "Updated Country",
            ContactName = "Jane Doe",
            ContactPhone = "+0987654321",
            ContactEmail = "janedoe@example.com",
            CreatedAt = client.CreatedAt,
            UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };

        // Act
        var result = await _controller.Update(updatedClient);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedClient = Assert.IsType<Client>(okResult.Value);
        Assert.Equal("Updated Client", returnedClient.Name);

        var clientInDb = await _context.Clients.FindAsync(1);
        Assert.Equal("Updated Client", clientInDb.Name);
    }
}
