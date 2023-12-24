using ECommerse.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerse.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController:ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomerController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }
       

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetAllCustomersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }
    }
}
