using System.ComponentModel.DataAnnotations;

namespace InternshipTask.Dtos;

public class UserRegister
{
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string? Password { get; set; }
    public string? ReturnUrl { get; set; }
}