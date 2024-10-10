namespace DealMeet.Core;

public class MessageDto
{
    public string Content { get; set; }
    
    public DateTime DateTime { get; set; }

    public Guid SenderId { get; set; }
    
    public Guid WhoId { get; set; }
}