using System.ComponentModel.DataAnnotations;

namespace InternshipTask.Models;

public class User
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string? Password { get; set; }
}