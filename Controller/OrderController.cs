

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        Order? holder = _orderService.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Order> holder = _orderService.GetAll();
        return Ok(holder);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        var result = _orderService.Post(order);
        if(result == null) return BadRequest("Time format for created at/updated is wrong or items in order do not exist");
        return Ok(order);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _orderService.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Order order)
    {
        var result = _orderService.Put(order);
         if(result == false) return BadRequest("Time format for created at/updated is wrong or items in order do not exist");
        return Ok(order);
    }
   
    [HttpGet("{OrderID}/Items")]
    public async Task<IActionResult> GetItemsInOrder(int OrderID)
    {
        var holder = _orderService.GetItemsInOrder(OrderID);
        if (holder == null) return NotFound($"Items for OrderID: {OrderID} not found");
        return Ok(holder);
    }
}



