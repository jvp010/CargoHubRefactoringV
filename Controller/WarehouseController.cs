using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class WarehouseController : GenericController<Warehouse>
{
    public WarehouseController(ICRUDinterface<Warehouse> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}