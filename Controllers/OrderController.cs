using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Получить все заказы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            if (orders == null || orders.Count == 0)
            {
                return NoContent();  // Возвращаем статус 204, если заказов нет
            }
            return Ok(orders);  // Возвращаем список заказов со статусом 200
        }

        // Получить заказ по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();  // Возвращаем статус 404, если заказ не найден
            }
            return Ok(order);  // Возвращаем заказ со статусом 200
        }

        // Добавить новый заказ
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] OrderDTO order)
        {
            if (order == null)
            {
                return BadRequest("Order data is null");  // Проверка на корректность данных
            }

            var createdOrder = await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);  // Возвращаем статус 201 с созданным заказом
        }

        // Обновить заказ
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> PutOrder(int id, [FromBody] OrderDTO order)
        {
            if (id != order.Id)
            {
                return BadRequest("Order ID mismatch");  // Возвращаем ошибку, если ID не совпадают
            }

            var updatedOrder = await _orderService.UpdateOrder(order);
            if (updatedOrder == null)
            {
                return NotFound();  // Возвращаем статус 404, если заказ не найден
            }
            return Ok(updatedOrder);  // Возвращаем обновленный заказ со статусом 200
        }

        // Удалить заказ
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _orderService.DeleteOrder(id);
            if (!success)
            {
                return NotFound();  // Возвращаем статус 404, если заказ не найден для удаления
            }
            return Ok();  // Возвращаем статус 200 при успешном удалении
        }
    }

}
