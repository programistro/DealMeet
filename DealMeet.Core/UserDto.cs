using System.ComponentModel.DataAnnotations;

namespace DealMeet.Core;

public class UserDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public string? Patronymic  { get; set; }
    
    [Required]
    public int Age { get; set; }
    
    [Required]
    public string Gender { get; set; }
    
    public string? About { get; set; }
    
    [Required]
    public DateOnly Born { get; set; }
    
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