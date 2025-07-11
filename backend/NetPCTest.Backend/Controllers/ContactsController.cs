using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.Backend.Data;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok(new {message = "Hello World!"});
    }
}