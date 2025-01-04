using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;


[ApiController]
[Route("api/[controller]")]
public class GenericController<T> : ControllerBase where T : BaseEntity
{
    private readonly ModelContext _context;
    private readonly ICRUDinterface<T> _CRUDinterface;

    public GenericController(ICRUDinterface<T> CRUDinterface, ModelContext context)
    {
        _CRUDinterface = CRUDinterface;
        _context = context;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetbyId(int id)
    {
        T holder = _CRUDinterface.Get(id);
        if (holder != null) return Ok(holder);
        return NotFound($"id {id} has not been found");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        List<T> holder = _CRUDinterface.GetAll();
        return Ok(holder);
    }
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] T Client)
    {
        if (_CRUDinterface.Post(Client) != null) return Ok(Client);
        return BadRequest("Time format for created at/updated is wrong"); 
        }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        bool check = _CRUDinterface.Delete(id);
        if (check) return Ok("id " + id + " has been deleted");
        return BadRequest($"id: {id} not found to be deleted");
    }






}