using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        // Here we first .Select to make sure EF doesn't create a SQL query with columns that we are not going to use
        // anyway. Then, we .Select to create the DTOs.
        var contacts = await context.Contacts
            .Select(c => new { c.Id, c.Name, c.Surname })
            .Select(c => ContactBriefDto.FromIdNameSurname(c.Id, c.Name, c.Surname))
            .ToListAsync(); 
        
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactCreationDto contactCreationDto)
    {
        var contact = ContactCreationDto.FromDto(contactCreationDto);
        
        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();
        
        return Ok();
    }
}