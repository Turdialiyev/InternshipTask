using Microsoft.AspNetCore.Identity;

namespace InternshipTask.Models;

public class TestUser : IdentityUser
{
    public override string? UserName { get; set; }
    public string? Password { get; set; }
    public string[]? Roles { get; set; }
}