using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class SuppliersController : GenericController<Supplier>
{
    public SuppliersController(ICRUDinterface<Supplier> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}