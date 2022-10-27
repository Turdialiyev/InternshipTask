using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using InternshipTask.Repositories;
using InternshipTask.Services;
using Xunit;
using InternshipTask.Data;

namespace unitTest;

public class ProductCalculate
{
    private readonly IConfigurationRoot _config;
    private readonly ProductService _productService;
    private readonly Mock<IProductRepository> _productRpository = new Mock<IProductRepository>();
    private readonly Mock<IProductHistoryRepository> _productHistoryRpository = new Mock<IProductHistoryRepository>();

    public ProductCalculate()
    {
        var logger = Mock.Of<ILogger<ProductService>>();
        _config = new ConfigurationBuilder().Build();

        _productService = new ProductService(logger, _productHistoryRpository.Object, _productRpository.Object, _config);
    }

    [Fact]
    public void GetReturnsAllCostOfProduct()
    {

        // Arrange
        int amount = 55;
        double price = 70;
        double vat =  _config.GetConnectionString("VAT") != null ? double.Parse(_config.GetConnectionString("VAT")) : 0;

        // Act
        var result = _productService.Calculate(vat, amount, price);
        var calculate = (decimal)((price * amount) * (1 + vat));

        // Assert
        Assert.Equal(result, calculate);
    }
}