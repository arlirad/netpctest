using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalisationController(ILocalisationService localisationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLocaleKeyStrings([Required] string locale)
    {
        var localeKeyStrings = await localisationService.GetLocaleKeyStrings(locale);
        
        if (localeKeyStrings.Count == 0)
            return NotFound();
        
        return Ok(localeKeyStrings);
    }
}