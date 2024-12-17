using Microsoft.AspNetCore.Components;

[Route("api/[Controller]")]
public class ClientController : GenericController<Client>
{
    public ClientController(ICRUDinterface<Client> CRUDinterface, ModelContext context) : base(CRUDinterface, context)
    {

    }
}