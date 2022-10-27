using InternshipTask.Models;

namespace InternshipTask.Services;

public interface IProductService
{
    public ValueTask<Result<IEnumerable<Product>>> GetAllAsync();
    public Result<Product> GetByIdAsync(ulong productId);
    public ValueTask<Result<Product>> DeleteProduct(ulong productId, string userId);
    public ValueTask<Result<Product>> UpdateProduct(ulong productId, Product product);
    public ValueTask<Result<Product>> CreateProduct(Product product);
    public decimal Calculate(double vat, int amount, double price);
    ValueTask<Result<IEnumerable<ProductHistory>>> GetProductHistoryAsync(DateTime? start, DateTime? end);

}