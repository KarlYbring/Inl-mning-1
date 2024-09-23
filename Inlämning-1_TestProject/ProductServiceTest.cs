using Inlämning_1.Interfaces;
using Inlämning_1.Models;
using Inlämning_1.Services;
using Moq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Inlämning_1_TestProject;

public class ProductServiceTest
{

    private readonly Mock<IProductService<Product, Product>> _mockProductService = new();
    [Fact]

    
    public void AddProductToList__Should_AddProductToListOfProducts__Return_succeeded()
    {

        //Arrange
        var product = new Product { ProductName = "Pasta", ProductPrice = 24, ProductId = Guid.NewGuid().ToString()};
        var expectedResponse = new ResultResponse<Product> { Succeeded = true , Result = product, Message = "Produkt har lagts till i listan" };
        _mockProductService.Setup(productService => productService.AddProduct(product)).Returns(expectedResponse);
        var productService = _mockProductService.Object;

        //Act
        var result = productService.AddProduct(product);
        
        //Assert

        Assert.True(result.Succeeded);
        Assert.Equal(product, result.Result);
    }
}
