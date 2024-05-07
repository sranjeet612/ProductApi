using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Repository
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();

        Task<Customer> GetCustomerByIdAsync(int Id);

        Task AddCustomerAsync(Customer customer);

        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<Customer> DeleteCustomerAsync(int id);
    }
}
