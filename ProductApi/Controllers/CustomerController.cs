using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;

namespace ProductApi.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customer;
        public CustomerController(ICustomerService customer)
        {
            this._customer = customer;
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return this.Ok(await this._customer.GetCustomersAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            return Id != null ? this.Ok(await this._customer.GetCustomerByIdAsync(Id)) : this.BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            await this._customer.AddCustomerAsync(customer);
            return this.Created("Customer created successfully", customer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            var result = await this._customer.UpdateCustomerAsync(customer);
            if (result != null)
                return this.Ok(customer);
            return this.BadRequest();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            var result = await this._customer.DeleteCustomerAsync(Id);
            if (result != null)
                return this.Ok(result);
            return this.BadRequest();
        }

    }
}
