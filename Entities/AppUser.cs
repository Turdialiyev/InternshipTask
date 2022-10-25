using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Project.Entities;

public class AppUser : IdentityUser 
{
  public string? FirstName { get; set; }
  public string? LAstName { get; set; }
}