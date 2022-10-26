using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;

namespace Project.Data;
public class AppDbContext : DbContext
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductHistory>? ProductHistories { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}