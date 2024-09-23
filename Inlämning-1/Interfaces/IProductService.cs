using Inlämning_1.Models;
namespace Inlämning_1.Interfaces;

public interface IProductService<T, TResult> where T : class where TResult : class
{
    ResultResponse<TResult> AddProduct(T product);
    ResultResponse<IEnumerable<Product>> GetProducts();
    ResultResponse<TResult> Delete(string productId);
}
