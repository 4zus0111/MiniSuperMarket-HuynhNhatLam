using Microsoft.AspNetCore.Mvc;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected NotFoundObjectResult NotFoundMessage(string message) => NotFound(new { message });

    protected BadRequestObjectResult BadRequestMessage(string message) => BadRequest(new { message });

    protected ConflictObjectResult ConflictMessage(string message) => Conflict(new { message });
}
