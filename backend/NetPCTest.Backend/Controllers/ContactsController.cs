using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/*
 * Controller responsible for the CRUD part of the application.
 * Has a rate limiter GetContacts to avoid (D)DOS attacks.
 */
[ApiController]
[Route("api/[controller]")]
public class ContactsController(IContactsService contactsService) : ControllerBase
{
    [HttpGet("count")]
    public async Task<IActionResult> GetContactCount(CancellationToken cancellationToken)
    {
        var count = await contactsService.GetContactCount(cancellationToken);
        
        return Ok(new { Count = count });
    }
    
    [EnableRateLimiting("list")]
    [HttpGet]
    public async Task<IActionResult> GetContacts(CancellationToken cancellationToken, int startIndex = 0, int count = 50)
    {
        var contacts = await contactsService.GetContacts(startIndex, count, cancellationToken);
        
        return Ok(contacts);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContactDetails([Required] int id, CancellationToken cancellationToken)
    {
        var contact = await contactsService.GetContact(id, cancellationToken);
        if (contact == null)
            return NotFound();
        
        return Ok(contact);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateContactDetails([Required] int id, [FromBody] ContactUpdateDto contactUpdateDto)
    {
        var result = await contactsService.UpdateContact(id, contactUpdateDto);

        return result switch
        {
            UpdateContactResult.Invalid => BadRequest(),
            UpdateContactResult.NotFound => NotFound(),
            UpdateContactResult.Success => Ok(),
            _ => BadRequest()
        };
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactCreationDto contactCreationDto)
    {
        var result = await contactsService.CreateContact(contactCreationDto);
        
        if (!result.Success)
            return BadRequest(result);
        
        return CreatedAtAction(nameof(GetContactDetails), new { id = result.Id }, result);
    }
}