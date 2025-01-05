using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        Inventory holder = _inventoryService.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        List<Inventory> holder = _inventoryService.GetAll();
        return Ok(holder);
    }
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] Inventory Client)
    {
        if (_inventoryService.Post(Client) != null) return Ok(Client);
        return BadRequest("Time format for created at/updated is wrong"); 
        }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _inventoryService.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Inventory Inventory)
    {
        bool check = _inventoryService.PutWithConstraints(Inventory);
        if(check) return Ok(Inventory);
        return BadRequest($"id: {Inventory.Id} not found so can not be modified");

    }
    [HttpGet("item/{ItemID}")]
    public async Task<IActionResult> InventoriesWithItem(string ItemID)
    {
        List<Inventory> holder = _inventoryService.GetInventoriesForItem(ItemID);
        if (holder.Count != 0) return Ok(holder);
        return NotFound($"ItemID {ItemID} has not been found");
    }
    [HttpGet("item/totals/{ItemID}")]
    public async Task<IActionResult> InventoryTotalsForItem(string ItemID)
    {
        (bool check, double[] Result) = _inventoryService.GetInventoryTotalsForItem(ItemID);
        if(check && Result.Length > 0){
            return Ok($"total_expected: {Result[0]},\ntotal_ordered: {Result[1]},\ntotal_allocated: {Result[2]},\ntotal_available: {Result[3]}");
        }
        return NotFound($"ItemID {ItemID} has not been found");
    }
}

    


