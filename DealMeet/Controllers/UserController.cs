using DealMeet.Core;
using DealMeet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealMeet.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly UserDbContext _context;

    public UserController(ILogger<UserController> logger, UserDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpGet("get-user")]
    public IActionResult GetUser(Guid id)
    {
        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);
        
        if (findUser != null)
        {
            return Ok(findUser);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("get-all-users")]
    public IActionResult GetAllUsers()
    {
        var users = _context.Users;

        return Ok(users);
    }

    [HttpPut("update-gender-user")]
    public IActionResult UpdateGenderUser(Guid id, string gender)
    {
        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
            return BadRequest();
        
        findUser.Gender = gender;

        _context.Users.Update(findUser);
        _context.SaveChanges();

        return Ok(findUser);
    }

    [HttpPut("update-hobby-user")]
    public IActionResult UpdateHobbyUser(Guid id, List<string> hobby)
    {
        if (hobby == null)
            return BadRequest();

        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
            return BadRequest();

        findUser.Hobby = hobby;
        _context.Users.Update(findUser);
        _context.SaveChanges();
        
        return Ok(findUser);
    }

    [HttpPut("update-user")]
    public IActionResult UpdateUser(Guid id, UserDto userDto)
    {
        if (userDto == null)
            return BadRequest();

        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
            return BadRequest();

        findUser.About = userDto.About;
        findUser.Online = userDto.Online;
        findUser.Gender = userDto.Gender;
        findUser.Hobby = userDto.Hobby;
        findUser.Age = userDto.Age;
        findUser.Patronymic = userDto.Patronymic;
        findUser.FirstName = userDto.FirstName;
        findUser.LastName = userDto.LastName;
        findUser.Born = userDto.Born;
        findUser.UsersChat = userDto.UsersChat;
        findUser.UserChatIgnore = userDto.UserChatIgnore;

        _context.Users.Update(findUser);
        _context.SaveChanges();

        return Ok(findUser);
    }

    [HttpPut("update-status")]
    public IActionResult UpdateStatus(Guid id, bool online)
    {
        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
            return BadRequest();

        findUser.Online = online;

        _context.Users.Update(findUser);
        _context.SaveChanges();

        return Ok(findUser);
    }

    [HttpPut("update-avatar-user")]
    public IActionResult UpdateAvatar(Guid id, string avatar)
    {
        var findUser = _context.Users.FirstOrDefault(x => x.Id == id);

        if (findUser == null)
        {
            return BadRequest();
        }

        findUser.Avatar = avatar;
        _context.Users.Update(findUser);
        _context.SaveChanges();

        return Ok(findUser);
    }
    
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<User>>> GetFilteredUsers([FromQuery]FilterUsersRequest request)
    {
        var users = await FilterUsersAsync(request.UserId);
        return Ok(users.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize));
    }

    private async Task<IEnumerable<User>> FilterUsersAsync(Guid userId)
    {
        var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
            return null;
            //throw new NotFoundException("Пользователь не найден");

        // Получаем всех пользователей из базы данных
        var allUsers = await _context.Users.ToListAsync();

        // Применяем фильтры на стороне клиента
        var filteredUsers = allUsers
            .Where(u => u.Hobby != null && u.Hobby.Intersect(currentUser.Hobby).Any())
            .OrderBy(u => Math.Abs(u.Age - currentUser.Age))
            .ToList(); // Переводим в список для дальнейшей обработки на стороне клиента

        // Применяем случайный порядок
        var shuffled = filteredUsers.OrderBy(x => Guid.NewGuid());

        // Применяем соотношение полов
        var genderRatio = currentUser.Gender == "man" ? 2 : 0.5;
        return ApplyGenderRatio(shuffled.ToList(), genderRatio);
    }


    private IEnumerable<User> ApplyGenderRatio(List<User> users, double ratio)
    {
        var result = new List<User>();
        foreach (var user in users)
        {
            if ((result.Count % 3 == 0 || result.Count == 0) && user.Gender == "woman")
                result.Add(user);
            else if (Random.Shared.NextDouble() < ratio)
                result.Add(user);
        }
        return result;
    }
}