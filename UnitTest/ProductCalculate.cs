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
        double vat =  0.002;

        // Act
        var result = (decimal)(_productService.Calculate(vat, amount, price));

        // Assert
        Assert.Equal(result, (decimal)3857.7);
    }
}