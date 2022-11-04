using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;

namespace InternshipTask.Data;
public class AppDbContext : AuditableIdentityContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<InternshipTask.Models.Product>? Products { get; set; }
}