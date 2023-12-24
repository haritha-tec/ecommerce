namespace ECommerse.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
