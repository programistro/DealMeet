using System.ComponentModel.DataAnnotations;

namespace DealMeet.Core;

public class Event
{
    [Key]
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string About { get; set; }
    
    public double Cost { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Adress { get; set; }
    
    public string? Image { get; set; }
}