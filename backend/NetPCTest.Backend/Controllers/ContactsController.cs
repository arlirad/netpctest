using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await context.Contacts.Select(c => new { c.Id, c.Name, c.Surname }).ToListAsync(); 
        
        return Ok(contacts);
    }
}