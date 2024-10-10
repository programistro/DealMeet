using DealMeet.Core;
using DealMeet.Data;
using Microsoft.AspNetCore.Mvc;

namespace DealMeet.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly ILogger<EventController> _logger;

    private readonly EventDbContext _context;

    public EventController(ILogger<EventController> logger, EventDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [HttpPost("create-event")]
    public IActionResult CreateEvent(EventDto eventDto)
    {
        if (eventDto == null)
            return BadRequest();

        Event _event = new()
        {
            Id = Guid.NewGuid(),
            About = eventDto.About,
            Cost = eventDto.Cost,
            Date = eventDto.Date,
            Image = eventDto.Image,
            Title = eventDto.Title,
            Adress = eventDto.Adress
        };

        _context.Events.Add(_event);
        _context.SaveChanges();
        
        return Ok(_event);
    }

    [HttpGet("get-event")]
    public IActionResult GetEvent(Guid id)
    {
        var findEvent = _context.Events.FirstOrDefault(x => x.Id == id);

        if (findEvent == null)
            return BadRequest();

        return Ok(findEvent);
    }

    [HttpPost("edit-event")]
    public IActionResult EditEvent(Guid id, EventDto eventDto)
    {
        if (eventDto == null)
            return BadRequest();

        var findEvent = _context.Events.FirstOrDefault(x => x.Id == id);

        if (findEvent == null)
            return BadRequest();

        findEvent.About = eventDto.About;
        findEvent.Cost = eventDto.Cost;
        findEvent.Adress = eventDto.Adress;
        findEvent.Image = eventDto.Image;
        findEvent.Date = eventDto.Date;
        findEvent.Title = eventDto.Title;

        _context.Events.Update(findEvent);
        _context.SaveChanges();

        return Ok(findEvent);
    }
}