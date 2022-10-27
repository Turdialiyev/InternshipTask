using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;
using InternshipTask.Repositories;

namespace InternshipTask.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IProductRepository _productRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;

    public ProductService(ILogger<ProductService> logger, IProductHistoryRepository productHistoryRepository, IProductRepository productRepository, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _productRepository = productRepository;
        _productHistoryRepository = productHistoryRepository;
    }

    public decimal Calculate(double vat, int amount, double price)
    {
        var calculate = (decimal)((amount * price) * (1 + vat));

        return calculate;
    }

    public async ValueTask<Result<Product>> CreateProduct(Product model)
    {

        if (model is null)
            return new("Product is invalid");

        var vat = double.Parse(_configuration.GetConnectionString("VAT"));


        model.CreatedAt = DateTime.Now;
        model.TotalPrice = Calculate(vat, model.Quantiy, model.Price);

        try
        {
            var createdProduct = await _productRepository.AddAsync(model);

            if (createdProduct is null)
                return new("Product is not created");

            return new(true) { Data = createdProduct };
        }
        catch (System.Exception)
        {
            throw new NotImplementedException();
        }
    }

    public async ValueTask<Result<Product>> DeleteProduct(ulong productId, string userId)
    {

        var existingProduct = _productRepository.GetById(productId);

        if (existingProduct is null)
            return new("Product with given Id Not Found");

        ProductHistory model = new ProductHistory()
        {
            Title = existingProduct.Title,
            Quantiy = existingProduct.Quantiy,
            Price = existingProduct.Price,
            TotalPrice = existingProduct.Quantiy,
            IsDeleted = true,
            ProductId = productId,
            UserId = new Guid(userId),
            CreatedAt = existingProduct.CreatedAt,
            UpdatedAt = DateTime.Now,
            DaletedAt = DateTime.Now
        };

        var createdProductHistory = _productHistoryRepository.AddAsync(model);

        try
        {
            var result = await _productRepository.Remove(existingProduct);

            return new(true) { Data = result };
        }
        catch (Exception e)
        {
            throw new("Couldn't delete Product. Plaese contact support", e);
        }
    }
    public async ValueTask<Result<IEnumerable<Product>>> GetAllAsync()
    {
        var products = await _productRepository.GetAll().ToListAsync();

        if (products is null)
            return new(false) { ErrorMessage = "Any Product not found" };

        try
        {
            return new(true) { Data = products };
        }
        catch (Exception e)
        {
            throw new("Couldn't get Products Please contact support", e);
        }
    }

    public Result<Product> GetByIdAsync(ulong productId)
    {
        if (productId < 1)
            return new("Given id invalid");
        try
        {
            var result = _productRepository.GetById(productId);

            if (result is null)
                return new("Product given with id Not Found");

            return new(true) { Data = result };
        }
        catch (Exception e)
        {
            throw new("Couldn't get Categories Please contact support", e);
        }
    }

    public async ValueTask<Result<IEnumerable<ProductHistory>>> GetProductHistoryAsync(DateTime? from, DateTime? to)
    {
        // var person = (from p in _productHistoryRepository.GetAll()
        //               join e in _productRepository.GetAll()
        //               on p.UserId equals e.UserId
        //               where p.FirstName == "KEN"
        //               select new
        //               {
        //                   ID = p.BusinessEntityID,
        //                   FirstName = p.FirstName,
        //                   MiddleName = p.MiddleName,
        //                   LastName = p.LastName,
        //                   EmailID = e.EmailAddress1
        //               }).ToList();

        var query = _productHistoryRepository.GetAll();

        if (from is not null)
        {
            query = query.Where(p => p.UpdatedAt > from);
        }

        if (to is not null)
        {
            query = query.Where(p => p.UpdatedAt <= to);
        }

        return new(true) { Data = query };
    }

    public async ValueTask<Result<Product>> UpdateProduct(ulong productId, Product model)
    {
        if (productId < 1)
            return new("Given Product Id invalid");

        if (model.UserId == null)
            return new("Given user Id invalid");

        var existingProduct = _productRepository.GetById(productId);

        if (existingProduct is null)
            return new("Product given Id not found");

        var createProductHistory = new ProductHistory()
        {
            Title = existingProduct.Title,
            Quantiy = existingProduct.Quantiy,
            Price = existingProduct.Price,
            TotalPrice = existingProduct.Quantiy,
            IsDeleted = false,
            ProductId = productId,
            UserId = existingProduct.UserId,
            CreatedAt = existingProduct.CreatedAt,
            UpdatedAt = DateTime.Now,
            DaletedAt = null,

        };

        var result = _productHistoryRepository.AddAsync(createProductHistory);

        var vat = double.Parse(_configuration.GetConnectionString("VAT"));
        var calculate = Calculate(vat, model.Quantiy, model.Price);

        existingProduct.TotalPrice = calculate;
        existingProduct.Title = model.Title;
        existingProduct.Quantiy = model.Quantiy;
        existingProduct.Price = model.Price;
        existingProduct.UserId = model.UserId;

        try
        {
            var updatedProduct = await _productRepository.Update(existingProduct);

            return new(true) { Data = updatedProduct };
        }
        catch (Exception e)
        {
            throw new("Couldn't update Product. Please contact support", e);
        }
    }
}
