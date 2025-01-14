using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemInterface _ItemInterface;

    public ItemController(ItemInterface _ItemInterface)
    {
        this._ItemInterface = _ItemInterface;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(string id)
    {
        Item? holder = _ItemInterface.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Item> holder = _ItemInterface.GetAll();
        return Ok(holder);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] Item item)
    {
        //(uid, time, itemgroup, itemline, itemtype, supplier, item itself )
        var result = _ItemInterface.Post(item);
        if (result.Item1 == false) return BadRequest("uid is empty");
        else if (result.Item2 == false) return BadRequest("Time format for created at/updated is wrong");
        else if (result.Item3 == false) return BadRequest("ItemGroup id in item is not found");
        else if (result.Item4 == false) return BadRequest("ItemLine id in item is not found");
        else if (result.Item5 == false) return BadRequest("ItemType id in item is not found");
        else if (result.Item6 == false) return BadRequest("Supplier id in item is not found");
        return Ok(item);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        bool check = _ItemInterface.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] Item item)
    {
        var result = _ItemInterface.Put(item);
        // (exist, itemgroup, itemline, itemtype, supplier)

        if (result.Item1 == false) return BadRequest($"id: {item.Uid} not found so can not be modified");
        else if (result.Item2 == false) return BadRequest("ItemGroup id in item is not found");
        else if (result.Item3 == false) return BadRequest("ItemLine id in item is not found");
        else if (result.Item4 == false) return BadRequest("ItemType id in item is not found");
        else if (result.Item5 == false) return BadRequest("Supplier id in item is not found");
        return Ok(item);
    }
    [HttpGet("ItemLines/{ItemID}")]
    public async Task<IActionResult> ItemLinesWithItem(int ItemID)
    {
        List<Item> holder = _ItemInterface.GetItemsForItemLine(ItemID);
        if (holder.Count != 0) return Ok(holder);
        return NotFound($"ItemID {ItemID} has not been found");
    }
    [HttpGet("ItemTypes/{ItemID}")]
    public async Task<IActionResult> ItemTypesWithItem(int ItemID)
    {
        List<Item> holder = _ItemInterface.GetItemsForItemType(ItemID);
        if (holder.Count != 0) return Ok(holder);
        return NotFound($"ItemID {ItemID} has not been found");
    }
    [HttpGet("ItemGroups/{ItemID}")]
    public async Task<IActionResult> ItemGroupsWithItem(int ItemID)
    {
        List<Item> holder = _ItemInterface.GetItemsForItemGroup(ItemID);
        if (holder.Count != 0) return Ok(holder);
        return NotFound($"ItemID {ItemID} has not been found");
    }
    [HttpGet("Suppliers/{SuppliersID}")]
    public async Task<IActionResult> ItemsForSupplier(int SuppliersID)
    {
        List<Item> holder = _ItemInterface.GetItemsForSupplier(SuppliersID);
        if (holder.Count != 0) return Ok(holder);
        return NotFound($"SuppliersID {SuppliersID} has not been found");
    }
}
