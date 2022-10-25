#pragma warning disable
using InternshipTask.Entities;

namespace InternshipTask.Data;

public class AppDbInitialize
{

    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

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
                        TotalPrice =131.2M,
                        UserId = new Guid(),
                        CreatedAt = DateTime.Now.ToString("MM/dd/yyyy")
                    },
                    new Product ()
                    {
                        Title = "HDD SSD 512GB ",
                        Quantiy = 102,
                        Price = 190.99,
                        TotalPrice =131.2M,
                        UserId = new Guid(),
                        CreatedAt = DateTime.Now.ToString("MM/dd/yyyy")
                    },
                    new Product ()
                    {
                        Title = " RAM DDR4 16GB",
                        Quantiy = 47,
                        Price = 80.32,
                        TotalPrice =131.2M,
                        UserId = new Guid(),
                        CreatedAt = DateTime.Now.ToString("MM/dd/yyyy")
                    },
                });
                context.SaveChanges();
            }
        }
    }
}