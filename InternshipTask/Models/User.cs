using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InternshipTask.Models;

public class User : IdentityUser
{
    [Required]
    public override string? UserName { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string? Password { get; set; }

    public string[]? Roles { get; set; }
}