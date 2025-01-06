using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class ItemGroupsController : GenericController<ItemGroup>
{
    public ItemGroupsController(ICRUDinterface<ItemGroup> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}