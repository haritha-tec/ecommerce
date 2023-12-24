using ECommerse.Api.Search.Models;

namespace ECommerse.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess,IEnumerable<Product> products,string ErrorMessage)> GetProductsAsync();
    }
}
