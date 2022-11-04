using InternshipTask.Data;
using InternshipTask.Models;

namespace InternshipTask.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public void SaveAudit(Product oldProduct, Product model) => _context.Entry(oldProduct).CurrentValues.SetValues(model);

    IQueryable<Audit> IProductRepository.GetAllAuditLogs()  => _context.Set<Audit>();
}