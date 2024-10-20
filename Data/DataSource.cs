using WebApplication1.Data.Models;

namespace WebApplication1.Data
{
    public class DataSource
    {
        private static DataSource instance;
        private DataSource()
        {
        }
        public static DataSource GetInstance()
        {
            if (instance == null)
            {
                instance = new DataSource();
            }
            return instance;
        }
        public List<Product> Products = new List<Product>();
        public List<Customer> Customers = new List<Customer>();
        public List<Order> Orders = new List<Order>();
    }

}

