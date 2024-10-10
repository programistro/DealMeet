using DealMeet.Core;
using DealMeet.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DealMeet.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    private readonly UserDbContext _context;

    public AuthController(ILogger<AuthController> logger, UserDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("singin-google")]
    public async Task<IActionResult> RegisterGoogle()
    {
        var prop = new AuthenticationProperties
        {
            RedirectUri = "/"
        };
        return Challenge(prop, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// поле born писать в формате "2024-02-12"
    /// </summary>
    [HttpPost("register-user")]
    public IActionResult RegisterUser(UserDto user)
    {
        if (user == null)
            return BadRequest();

        User newUser = new()
        {
            Id = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Born = user.Born,
            Patronymic = user.Patronymic,
            Avatar = user.Avatar,
            Age = user.Age,
            Gender = user.Gender
        };
        
        _context.Users.Add(newUser);
        _context.SaveChanges();
        
        return Ok(newUser);
    }
    
    [HttpDelete("delete-user")]
    public IActionResult DeleteUser(Guid id)
    {
        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
            return BadRequest();

        _context.Users.Remove(findUser);
        _context.SaveChanges();

        return Ok();
    }
}