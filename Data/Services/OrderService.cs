using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services
{
    public class OrderService
    {
        // Метод для добавления нового заказа
        public async Task<Order> AddOrder(Order order)
        {
            DataSource.GetInstance().Orders.Add(order);
            return await Task.FromResult(order);
        }

        // Метод для получения списка всех заказов
        public async Task<List<Order>> GetOrders()
        {
            return await Task.FromResult(DataSource.GetInstance().Orders);
        }

        // Метод для получения заказа по ID
        public async Task<Order?> GetOrder(int id)
        {
            var order = DataSource.GetInstance().Orders.FirstOrDefault(o => o.Id == id);
            return await Task.FromResult(order);
        }

        // Метод для обновления заказа
        public async Task<Order?> UpdateOrder(Order newOrder)
        {
            var order = DataSource.GetInstance().Orders.FirstOrDefault(o => o.Id == newOrder.Id);
            if (order != null)
            {
                order.CustomerId = newOrder.CustomerId;
                order.ProductIds = newOrder.ProductIds;
                order.OrderDate = newOrder.OrderDate;
                order.TotalAmount = newOrder.TotalAmount;
                return await Task.FromResult(order);
            }
            return null;
        }

        // Метод для удаления заказа
        public async Task<bool> DeleteOrder(int id)
        {
            var order = DataSource.GetInstance().Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                DataSource.GetInstance().Orders.Remove(order);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

    }
}
