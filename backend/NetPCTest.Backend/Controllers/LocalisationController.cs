using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/*
 * Controller responsible for providing localisation dictionaries to the frontend.
 * Has rate limiting on GetLocaleKeyStrings to avoid (D)DOS attacks. 
 */
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
    
    [EnableRateLimiting("locale")]
    [HttpGet("{locale}")]
    public async Task<IActionResult> GetLocaleKeyStrings(string locale)
    {
        var localeKeyStrings = await localisationService.GetLocaleKeyStrings(locale);
        
        if (localeKeyStrings.Count == 0)
            return NotFound();
        
        return Ok(localeKeyStrings);
    }
}