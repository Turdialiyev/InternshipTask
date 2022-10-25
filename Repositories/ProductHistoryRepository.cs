using InternshipTask.Data;
using InternshipTask.Entities;

namespace InternshipTask.Repositories;

public class ProductHistoryRepository : GenericRepository<ProductHistory>, IProductHistoryRepository
{
    public ProductHistoryRepository(AppDbContext context) : base(context) { }
}