using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/// <summary>
/// Implements logic used for the main CRUD part of the application.
/// Applies a rate limiter on GetContacts, in order to mitigate possible (D)DOS attacks.
/// </summary>
/// <param name="contactsService"><see cref="IContactsService"/> for contact data access.</param>
[ApiController]
[Route("api/[controller]")]
public class ContactsController(IContactsService contactsService) : ControllerBase
{
    [HttpGet("count")]
    public async Task<IActionResult> GetContactCount(CancellationToken cancellationToken)
    {
        var count = await contactsService.GetContactCountAsync(cancellationToken);
        
        return Ok(new { Count = count });
    }
    
    [EnableRateLimiting("list")]
    [HttpGet]
    public async Task<IActionResult> GetContacts(CancellationToken cancellationToken, int startIndex = 0, int count = 50)
    {
        var contacts = await contactsService.GetContactsAsync(startIndex, count, cancellationToken);
        
        return Ok(contacts);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContactDetails([Required] int id, CancellationToken cancellationToken)
    {
        var contact = await contactsService.GetContactAsync(id, cancellationToken);
        if (contact == null)
            return NotFound();
        
        return Ok(contact);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateContactDetails([Required] int id, [FromBody] ContactUpdateDto contactUpdateDto)
    {
        var result = await contactsService.UpdateContactAsync(id, contactUpdateDto);

        return result switch
        {
            UpdateContactResult.Invalid => BadRequest(),
            UpdateContactResult.NotFound => NotFound(),
            UpdateContactResult.Success => Ok(),
            _ => BadRequest()
        };
    }
    
    [Authorize]
    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> UpdateContactPassword([Required] int id, 
        [FromBody] ContactPasswordChangeDto contactPasswordChangeDto)
    {
        var result = await contactsService.SetContactPasswordAsync(id, contactPasswordChangeDto);

        return result ? Ok() : BadRequest();
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteContact([Required] int id)
    {
        var result = await contactsService.DeleteContactAsync(id);

        return result ?  Ok() : BadRequest();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactCreationDto contactCreationDto)
    {
        var result = await contactsService.CreateContactAsync(contactCreationDto);
        
        if (!result.Success)
            return BadRequest(result);
        
        return CreatedAtAction(nameof(GetContactDetails), new { id = result.Id }, result);
    }
}