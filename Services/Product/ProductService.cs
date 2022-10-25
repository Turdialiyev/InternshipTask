using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;
using InternshipTask.Repositories;

namespace InternshipTask.Services;

public partial class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public decimal Calculate(double vat, int amount, double price)
    {
        var calculate = (decimal)((amount * price) * (1 + vat));

        return calculate;
    }

    public async ValueTask<Result<Product>> CreateProduct(Product product)
    {

        if (product is null)
            return new("Product is invalid");

        var vat = double.Parse(_configuration.GetConnectionString("VAT"));


        var calculate = Calculate(vat, product.Quantiy, product.Price);

        var createdEntity = new InternshipTask.Entities.Product(product, DateTime.Now.ToString("yyyy-MM-dd"), calculate);

        try
        {
            var createdProduct = await _unitOfWork.Products.AddAsync(createdEntity);

            if (createdProduct is null)
                return new("Product is not created");

            return new(true) { Data = ToModel(createdProduct) };
        }
        catch (System.Exception)
        {
            throw new NotImplementedException();
        }
    }

    public async ValueTask<Result<Product>> DeleteProduct(ulong productId)
    {

        var existingProduct = _unitOfWork.Products.GetById(productId);

        if (existingProduct is null)
            return new("Product with given Id Not Found");

        var entity = new Entities.ProductHistory(existingProduct, null, DateTime.Now.ToString("yyyy-MM-dd"), true);
        var createdProductHistory = _unitOfWork.ProductRepositories.AddAsync(entity);

        try
        {
            var result = await _unitOfWork.Products.Remove(existingProduct);

            return new(true) { Data = ToModel(result) };
        }
        catch (Exception e)
        {
            throw new("Couldn't delete Product. Plaese contact support", e);
        }
    }
    public async ValueTask<Result<IEnumerable<Product>>> GetAllAsync()
    {
        var products = await _unitOfWork.Products.GetAll().ToListAsync();

        if (products is null)
            return new(false) { ErrorMessage = "Any Product not found" };

        try
        {
            return new(true) { Data = products.Select(ToModel).ToList() };
        }
        catch (Exception e)
        {
            throw new("Couldn't get Categories Please contact support", e);
        }
    }

    public Result<Product> GetByIdAsync(ulong productId)
    {
        if (productId < 1)
            return new("Given id invalid");
        try
        {
            var result = _unitOfWork.Products.GetById(productId);

            if (result is null)
                return new("Product given with id Not Found");

            return new(true) { Data = ToModel(result) };
        }
        catch (Exception e)
        {
            throw new("Couldn't get Categories Please contact support", e);
        }
    }

    public async ValueTask<Result<Product>> UpdateProduct(ulong productId, Product model)
    {
        if (productId < 1)
            return new("Given Product Id invalid");

        if (model.UserId == null)
            return new("Given user Id invalid");

        var exixtingProduct = _unitOfWork.Products.GetById(productId);

        if (exixtingProduct is null)
            return new("User given Id not found");

        var createProductHistory = new Entities.ProductHistory(exixtingProduct, DateTime.Now.ToString("yyyy-MM-dd"));
        var result = _unitOfWork.ProductRepositories.AddAsync(createProductHistory);
        var vat = double.Parse(_configuration.GetConnectionString("VAT"));

        var calculate = Calculate(vat, model.Quantiy, model.Price);
        exixtingProduct.TotalPrice = calculate;
        exixtingProduct.Title = model.Title;
        exixtingProduct.Quantiy = model.Quantiy;
        exixtingProduct.Price = model.Price;
        exixtingProduct.UserId = model.UserId;
        exixtingProduct.CreatedAt = null;

        try
        {
            var updated = await _unitOfWork.Products.Update(exixtingProduct);

            return new(true) { Data = ToModel(updated) };
        }
        catch (Exception e)
        {
            throw new("Couldn't update Product. Please contact support", e);
        }
    }
}
