using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class ItemLinesController : GenericController<ItemLine>
{
    public ItemLinesController(ICRUDinterface<ItemLine> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}