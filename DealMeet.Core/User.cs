using System.ComponentModel.DataAnnotations;

namespace DealMeet.Core;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string? Patronymic  { get; set; }
    
    public string? About { get; set; }
    
    public DateOnly Born { get; set; }
    
    public int Age { get; set; }
    
    public string Gender { get; set; }
    
    public List<string>? Hobby { get; set; }
    
    /// <summary>
    /// аватарка должна быть в формате base64
    /// </summary>
    public string? Avatar { get; set; } 
    
    /// <summary>
    /// Тут хранятся Id пользователей с кем он переписывался
    /// </summary>
    public List<string>? UsersChat { get; set; }
    
    /// <summary>
    /// Удаленные чаты
    /// </summary>
    public List<string>? UserChatIgnore { get; set; }
    
    public bool? Online { get; set; }
}