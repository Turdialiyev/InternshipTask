using InternshipTask.Models;

namespace InternshipTask.Services;

public interface IProductService
{
    public ValueTask<Result<IEnumerable<Product>>> GetAllAsync();
    public Result<Product> GetByIdAsync(int productId);
    public ValueTask<Result<Product>> DeleteProduct(int productId, string userId);
    public ValueTask<Result<Product>> UpdateProduct(int productId, string user, Product product);
    public ValueTask<Result<Product>> CreateProduct(string user, Product product);
    public decimal Calculate(double vat, int amount, double price);
    public ValueTask<Result<IEnumerable<Object>>> GetAduitByDate(DateTime? from, DateTime? to);

}