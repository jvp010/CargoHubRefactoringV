using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class ItemTypesController : GenericController<ItemType>
{
    public ItemTypesController(ICRUDinterface<ItemType> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}