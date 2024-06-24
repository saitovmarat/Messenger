using System.ComponentModel.DataAnnotations;


namespace Messenger.Core.Schemas;

public class LoginUserSchema
{
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    public string? Password { get; set; }
}