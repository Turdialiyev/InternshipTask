using InternshipTask.Data;
using InternshipTask.Models;

namespace InternshipTask.Repositories;

public class ProductHistoryRepository : GenericRepository<ProductHistory>, IProductHistoryRepository
{
    public ProductHistoryRepository(AppDbContext context) : base(context) { }
}