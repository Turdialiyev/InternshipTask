using InternshipTask.Data;
using InternshipTask.Models;

namespace InternshipTask.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }
}