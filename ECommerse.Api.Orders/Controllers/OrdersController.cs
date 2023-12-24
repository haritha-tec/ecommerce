using ECommerse.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerse.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private IOrdersProvider ordersProvider;
        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.orders);
            }

            return NotFound();
        }




    }
}
