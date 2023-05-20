using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;

namespace InternshipTask.Data;
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<InternshipTask.Models.Product>? Products { get; set; }
    public DbSet<InternshipTask.Models.Furniture>? Furnitures { get; set; }
}