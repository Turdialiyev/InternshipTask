
using InternshipTask.Models;

namespace InternshipTask.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    void SaveAudit(Product oldProduct, Product model);
    IQueryable<Audit> GetAllAuditLogs();  
      
}