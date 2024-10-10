using DealMeet.Core;
using DealMeet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealMeet.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly ILogger<ChatController> _logger;

    private readonly ChatDbContext _context;

    public ChatController(ILogger<ChatController> logger, ChatDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("send-message")]
    public IActionResult SendMessage(MessageDto message)
    {
        if (message == null)
        {
            return BadRequest();
        }

        Message newMessage = new()
        {
            Id = new Guid(),
            Content = message.Content,
            DateTime = message.DateTime,
            SenderId = message.SenderId,
            WhoId = message.WhoId
        };

        _context.Messages.Add(newMessage);
        _context.SaveChanges();
        
        return Ok(newMessage);
    }

    [HttpGet("get-message-all-user")]
    public async Task<IEnumerable<Message>> GetMessageAllUser(Guid sender, Guid who)
    {
        return await _context.Messages
            .Where(x => (x.SenderId == sender) && (x.WhoId == who))
            .ToListAsync();
    }
}