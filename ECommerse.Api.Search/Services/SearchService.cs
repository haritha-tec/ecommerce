using ECommerse.Api.Search.Interfaces;

namespace ECommerse.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        public SearchService(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var productsResult = await productService.GetProductsAsync();

            if(orderResult.IsSuccess)
            {
                foreach(var order in orderResult.orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.PrdoductName = productsResult.IsSuccess ?
                            productsResult.products.FirstOrDefault(p => p.Id == item.Id)?.Name :
                            "product Info is not available";
                    }
                }

                var result = new
                {
                    orders = orderResult.orders
                };

                return (true, result);

            }

            return (false, null);
        }
    }
}
