using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;

namespace InternshipTask.Data;
public class AppDbContext : IdentityDbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductHistory>? ProductHistories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}