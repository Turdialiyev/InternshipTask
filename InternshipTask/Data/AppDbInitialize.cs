#pragma warning disable
using InternshipTask.Models;
using Microsoft.AspNetCore.Identity;

namespace InternshipTask.Data;

public class AppDbInitialize
{

    public static async Task Seed(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        using var scope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var users = config.GetSection("Identity:IdentityServer:TestUsers").Get<User[]>();
        var roles = config.GetSection("Identity:IdentityServer:Roles").Get<string[]>();

        context.Database.EnsureCreated();

        // Products
        if (!context.Products.Any())
        {
            context.Products.AddRange(new List<Product>()
                {
                    new Product ()
                    {
                        Title = "HDD 1TB ",
                        Quantiy = 55,
                        Price = 74.09,
                        TotalPrice = (decimal)((1.002) * (55 * 74.09)),
                    },
                    new Product ()
                    {
                        Title = "HDD SSD 512GB ",
                        Quantiy = 102,
                        Price = 190.99,
                        TotalPrice = (decimal)((1.002) * (102 * 190.99)),
                    },
                    new Product ()
                    {
                        Title = " RAM DDR4 16GB",
                        Quantiy = 47,
                        Price = 80.32,
                        TotalPrice =(decimal)((1.002) * (47 * 80.32)),
                    },
                });
            context.SaveChanges();
        }
        // Added Admin Roles

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var newRole = new IdentityRole(role);
                var result = await roleManager.CreateAsync(newRole);
            }
        }

        // Added Admin UserName

        foreach (var user in users)
        {
            Guid guid = Guid.NewGuid();
            var newUser = new IdentityUser(user.UserName);
            var result = await userManager.CreateAsync(newUser, user.Password);
            var roleResult = await userManager.AddToRolesAsync(newUser, user.Roles);
        }

    }
}