using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly TransferService _transferService;

    public TransferController(TransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        Transfer? holder = _transferService.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Transfer> holder = _transferService.GetAll();
        return Ok(holder);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] Transfer transfer)
    {
        var result = _transferService.Post(transfer);
        if(result == null) return BadRequest("Time format for created at/updated is wrong or items in transfer do not exist");
        return Ok(transfer);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _transferService.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Transfer transfer)
    {
        var result = _transferService.Put(transfer);
         if(result == false) return BadRequest("Time format for created at/updated is wrong or items in transfer do not exist");
        return Ok(transfer);
    }
   
    
    [HttpGet("{TransferID}/items")]
    public async Task<IActionResult> GetItemsInTransfer(int TransferID)
    {
        List<TransferItem> items = _transferService.GetItemsInTransfer(TransferID);
        if (items == null) return NotFound($"Items for TransferID {TransferID} not found");
        return Ok(items);
    }
}
