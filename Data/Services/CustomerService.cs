using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services
{
    public class CustomerService
    {
        // Метод для добавления нового клиента
        public async Task<Customer> AddCustomer(Customer customer)
        {
            DataSource.GetInstance().Customers.Add(customer);
            return await Task.FromResult(customer);
        }

        // Метод для получения списка всех клиентов
        public async Task<List<Customer>> GetCustomers()
        {
            return await Task.FromResult(DataSource.GetInstance().Customers);
        }

        // Метод для получения клиента по ID
        public async Task<Customer?> GetCustomer(int id)
        {
            var customer = DataSource.GetInstance().Customers.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(customer);
        }

        // Метод для обновления клиента
        public async Task<Customer?> UpdateCustomer(Customer newCustomer)
        {
            var customer = DataSource.GetInstance().Customers.FirstOrDefault(c => c.Id == newCustomer.Id);
            if (customer != null)
            {
                customer.FullName = newCustomer.FullName;
                customer.Email = newCustomer.Email;
                customer.Address = newCustomer.Address;
                return await Task.FromResult(customer);
            }
            return null;
        }

        // Метод для удаления клиента
        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = DataSource.GetInstance().Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                DataSource.GetInstance().Customers.Remove(customer);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }

}
