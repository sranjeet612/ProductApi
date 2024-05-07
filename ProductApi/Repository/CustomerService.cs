using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Repository
{
    public class CustomerService : ICustomerService
    {
        private readonly APIDBContext _dbContext;
        public CustomerService(APIDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (customer is Customer)
            {
                this._dbContext.Customers.Add(customer);
                await this._dbContext.SaveChangesAsync();
            }
        }

        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            Customer customer = await this._dbContext.Customers.FindAsync(id);
            if (customer != null)
            {
                this._dbContext.Customers.Remove(customer);
                await this._dbContext.SaveChangesAsync();
                return customer;
            }
            return null;
        }

        public async Task<Customer> GetCustomerByIdAsync(int Id)
        {
            var customer = await this._dbContext.Customers.FindAsync(Id);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await this._dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            if (customer is Customer)
            {
                Customer data = await this._dbContext.Customers.FindAsync(customer.Id);
                if (data != null)
                {
                    data.Name=customer.Name;
                    data.Email=customer.Email;
                    data.Phone=customer.Phone;
                    data.Address=customer.Address;
                    data.Gender=customer.Gender;    

                    await this._dbContext.SaveChangesAsync();
                    return data;
                }
            }

            return null;


        }
    }
}
