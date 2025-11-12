using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class ItemLineControllerIntegrationTests
{
    private readonly ModelContext _context;
    private readonly GenericController<ItemLine> _controller;

    public ItemLineControllerIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ModelContext>()
            .UseInMemoryDatabase("TestDatabase") //
            .Options;

        _context = new ModelContext(options); 
        var crudService = new CrudService<ItemLine>(_context); 
        _controller = new GenericController<ItemLine>(crudService, _context);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WhenItemsExist()
    {
        // Arrange
        var itemLines = new List<ItemLine>
        {
            new ItemLine { Id = 1, Name = "Fashion", Description = "Trendy clothing" },
            new ItemLine { Id = 2, Name = "Electronics", Description = "Gadgets and devices" }
        };
        _context.ItemLines.AddRange(itemLines);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedItemLines = Assert.IsType<List<ItemLine>>(okResult.Value);
        Assert.Equal(2, returnedItemLines.Count);
    }

}
