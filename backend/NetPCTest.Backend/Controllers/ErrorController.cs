using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace NetPCTest.Backend.Controllers;

[ApiController]
public class ErrorController : Controller
{
    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;
        
        return Problem(
            statusCode: 500,
            title: "ui.unexpected_error", 
            detail: exception?.Message 
        );
    }
}