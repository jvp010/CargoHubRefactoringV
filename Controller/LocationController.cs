using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly LocationService _locationService;

    public LocationController(LocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        Location? holder = _locationService.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Location> holder = _locationService.GetAll();
        return Ok(holder);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] Location location)
    {
        var result = _locationService.Post(location);
        if(result == null) return BadRequest("Time format for created at/updated is wrong or items in location do not exist");
        return Ok(location);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _locationService.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Location location)
    {
        var result = _locationService.Put(location);
         if(result == false) return BadRequest("Time format for created at/updated is wrong or items in location do not exist");
        return Ok(location);
    }
   
    [HttpGet("{LocationID}/Warehouses")]
    public async Task<IActionResult> GetLocationsInWarehouse(int LocationID)
    {
        var holder = _locationService.GetLocationsInWarehouse(LocationID);
        if (holder.Count == 0) return NotFound($"Locations for WarehouseID: {LocationID} not found");
        return Ok(holder);
    }
}
