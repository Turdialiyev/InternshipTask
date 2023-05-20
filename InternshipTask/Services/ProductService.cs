using Microsoft.EntityFrameworkCore;
using InternshipTask.Models;
using InternshipTask.Repositories;
using Microsoft.AspNetCore.Identity;
using InternshipTask.Data;
using System.Security.Claims;

namespace InternshipTask.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IProductRepository _productRepository;

    public ProductService(ILogger<ProductService> logger,
                        IProductRepository productRepository,
                        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _productRepository = productRepository;
    }

    public decimal Calculate(double vat, int amount, double price)
    {
        var calculate = (decimal)((amount * price) * (1 + vat));

        return calculate;
    }

    public async ValueTask<Result<Product>> CreateProduct(string userid, Product model)
    {
        if (model is null)
            return new("Product is invalid");

        var vat = double.Parse(_configuration.GetConnectionString("VAT"));

        model.TotalPrice = Calculate(vat, model.Quantiy, model.Price);

        try
        {
            var createdProduct = await _productRepository.AddAsync(model);

            if (createdProduct is null)
                return new("Product is not created");

            return new(true) { Data = createdProduct };
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async ValueTask<Result<Product>> DeleteProduct(int productId, string userId)
    {

        var existingProduct = _productRepository.GetById(productId);

        try
        {
            var result = await _productRepository.Remove(existingProduct!);

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

    public Result<Product> GetByIdAsync(int productId)
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

    public async ValueTask<Result<Product>> UpdateProduct(int productId, string user, Product model)
    {
        try
        {
            var oldProduct = _productRepository.GetById(productId);

            if (!ProductExists(model.Id))
            {
                return new("Product not found");
            }


            var vat = double.Parse(_configuration.GetConnectionString("VAT"));

            model.TotalPrice = Calculate(vat, model.Quantiy, model.Price);

            _productRepository.SaveAudit(oldProduct!, model);


            return new(true) { Data = model };
        }
        catch (Exception e)
        {
            throw new("Couldn't update Product. Please contact support", e);
        }
    }
    public async ValueTask<Result<IEnumerable<Object>>> GetAduitByDate(DateTime? from, DateTime? to)
    {
          var query = _productRepository.GetAllAuditLogs();

        if (from is not null)
            query = query.Where(p => p.DateTime > from);

        if (to is not null)
            query = query.Where(p => p.DateTime <= to);

        return new(true) { Data = query };
    }

    private bool ProductExists(int id)
    {
        return _productRepository.GetById(id) != null ? true : false;
    }
}
