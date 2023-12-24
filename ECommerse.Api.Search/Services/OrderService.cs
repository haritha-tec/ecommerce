using ECommerse.Api.Search.Interfaces;
using ECommerse.Api.Search.Models;
using System.Text.Json;

namespace ECommerse.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logger;
        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order>? orders, string? ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/orders/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            


        }
    }
}
