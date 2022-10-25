using InternshipTask.Data;

namespace InternshipTask.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; set; }
    public IProductHistoryRepository ProductRepositories { get; set; }
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Products = new ProductRepository(context);
        ProductRepositories = new ProductHistoryRepository(context);        
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    int Save() => _context.SaveChanges();
}