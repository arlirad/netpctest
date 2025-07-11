using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(IContactsService contactsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetContacts(int startIndex = 0, int count = 50)
    {
        var contacts = await contactsService.GetContacts(startIndex, count);
        
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> AddContact([FromBody] ContactCreationDto contactCreationDto)
    {
        var result = await contactsService.CreateContact(contactCreationDto);
        
        if (!result.Success)
            return BadRequest(result);
        
        return Ok();
    }
}