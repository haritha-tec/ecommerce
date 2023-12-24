namespace ECommerse.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {        
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetAllCustomersAsync();
    }
}
