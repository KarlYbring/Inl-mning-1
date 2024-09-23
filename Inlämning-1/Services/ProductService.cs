using Inlämning_1.Interfaces;
using Inlämning_1.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Inlämning_1.Services;

public class ProductService : IProductService<Product, Product>
{
    private List<Product> _products = [];
    private readonly IFileService _fileService;

    ProductService(string filepath)
    {
        _fileService = new FileService(filepath);
        GetProducts();

    }
    public ResultResponse<Product> AddProduct(Product product)
    {
        try
        {

            if (string.IsNullOrEmpty(product.ProductName))
            {
                return new ResultResponse<Product> { Succeeded = false, Message = "Du måste ange ett produktnamn" };
            }
            if (_products.Any(var => var.ProductName.ToLower() == product.ProductName.ToLower()))
            {
                return new ResultResponse<Product> { Succeeded = false, Message = "Varan finns redan i systemet" };

            }
            _products.Add(product);
            var json = JsonConvert.SerializeObject(product);
            var result = _fileService.SaveToFile(json);
            if (result.Succeeded)
            {
                return new ResultResponse<Product> { Succeeded = true, Message = "Produkt har sparats" };
            }
            else
            {
                return new ResultResponse<Product> { Succeeded = false, Message = result.Message};
            }      
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return new ResultResponse<Product> { Succeeded = false, Message = ex.Message };
        }
    }
    public ResultResponse<IEnumerable<Product>> GetProducts()
    {
        try
        {
            var result = _fileService.GetFromFile();
            if (result.Succeeded)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(result.Result!)!;
                return new ResultResponse<IEnumerable<Product>> { Succeeded = true, Result = _products };
            }
            else
            {
                return new ResultResponse<IEnumerable<Product>> { Succeeded = false, Message = result.Message };
            }

        }
        catch (Exception ex)
        {
            return new ResultResponse<IEnumerable<Product>> { Succeeded = false, Message = ex.Message };
        }
    }
    public ResultResponse<Product> Delete(string productId)
    {
        throw new NotImplementedException();
    }
}
