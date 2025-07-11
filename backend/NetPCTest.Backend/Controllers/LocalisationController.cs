using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalisationController(ILocalisationService localisationService) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetLocales()
    {
        var locales = await localisationService.GetAllLocales();
        
        if (locales.Count == 0)
            return NotFound();
        
        return Ok(locales);
    }
    
    [HttpGet("{locale}")]
    public async Task<IActionResult> GetLocaleKeyStrings(string locale)
    {
        var localeKeyStrings = await localisationService.GetLocaleKeyStrings(locale);
        
        if (localeKeyStrings.Count == 0)
            return NotFound();
        
        return Ok(localeKeyStrings);
    }
}