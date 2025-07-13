using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/*
 * Controller responsible for authenticating users (using the Contacts database).
 * Has rate limiting on everything in order to avoid brute-force attacks.
 */
[ApiController]
[Route("api/[controller]")]
public class AuthController(IRepository repository, IPasswordService passwordService, IConfiguration config) : Controller
{
    [EnableRateLimiting("auth")]
    [Authorize]
    [HttpGet]
    public IActionResult WhoAmI()
    {
        var email = User.Identity?.Name;
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        
        if (email is null || idClaim is null || !int.TryParse(idClaim.Value, out var id))
            return Unauthorized();
        
        return Ok(new WhoAmIResult()
        {
            Id = id,
            Email = email,
        });
    }
    
    [EnableRateLimiting("auth")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var contact = await repository.GetContactByEmail(loginDto.Email, cancellationToken);
        if (contact == null)
            return Unauthorized("invalid.credentials");
        
        var passwordCheckResult = passwordService.ComparePassword(contact, contact.PasswordHash, loginDto.Password);
        
        switch (passwordCheckResult)
        {
            case PasswordVerificationResult.Failed:
                return Unauthorized("invalid.credentials");
            case PasswordVerificationResult.SuccessRehashNeeded:
                contact.PasswordHash = passwordService.HashPassword(contact, loginDto.Password);
                await repository.UpdateContact(contact.Id, contact);
                break;
            case PasswordVerificationResult.Success:
                break;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Secret"] ?? throw new NullReferenceException());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, contact.Id.ToString()), 
                new Claim(ClaimTypes.Name, contact.Email),
            ]),
            Issuer = config["Jwt:Issuer"] ?? throw new NullReferenceException(),
            Audience = config["Jwt:Audience"] ?? throw new NullReferenceException(),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(config["Jwt:ExpirationMinutes"] ?? "60")),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature
            ),
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return Ok(new
        {
            token = jwt
        });
    }
}