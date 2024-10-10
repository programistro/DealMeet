using System.ComponentModel.DataAnnotations;

namespace DealMeet.Core;

public class Message
{
    [Key]
    public Guid Id { get; set; }
    
    
    public string Content { get; set; }
    
    public DateTime DateTime { get; set; }
    
    public Guid SenderId { get; set; }
    
    public Guid WhoId { get; set; }
}