using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentService _shipmentService;

    public ShipmentController(ShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet("{shipmentId}/items")]
    public ActionResult<List<ShipmentItem>> GetItemsInShipment(int shipmentId)
    {
        var items = _shipmentService.GetItemsInShipment(shipmentId);
        if (items == null) return NotFound();
        return items;
    }

    // [HttpPut("{shipmentId}/items")]
    // public ActionResult UpdateItemsInShipment(int shipmentId, List<ShipmentItem> items)
    // {
    //     var result = _shipmentService.UpdateItemsInShipment(shipmentId, items);
    //     if (!result) return NotFound();
    //     return NoContent();
    // }

    [HttpPost]
    public ActionResult<Shipment> Post(Shipment target)
    {
        var result = _shipmentService.Post(target);
        if (result == null) return BadRequest("Invalid data");
        return CreatedAtAction(nameof(GetItemsInShipment), new { shipmentId = result.Id }, result);
    }

    [HttpPut]
    public ActionResult Put(Shipment target)
    {
        var result = _shipmentService.Put(target);
        if (!result) return BadRequest("Invalid data");
        return NoContent();
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        Shipment? holder = _shipmentService.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        List<Shipment> holder = _shipmentService.GetAll();
        return Ok(holder);
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _shipmentService.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }
}
