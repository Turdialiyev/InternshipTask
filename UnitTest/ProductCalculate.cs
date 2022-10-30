using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using InternshipTask.Repositories;
using InternshipTask.Services;
using Xunit;

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
        _config = new ConfigurationBuilder().AddJsonFile($"appsettings.Development.json", optional: false).Build();
        _productService = new ProductService(logger, _productHistoryRpository.Object, _productRpository.Object, _config);
    }

    [Fact]
    public void GetReturnsAllCostOfProduct()
    {

        // Arrange
        int amount = 55;
        double price = 70;
        var vat = _config.GetConnectionString("VAT");

        // Act
        var result = (decimal)(_productService.Calculate(double.Parse(vat), amount, price));

        // Assert
        Assert.Equal(result, (decimal)3857.7);
    }
}