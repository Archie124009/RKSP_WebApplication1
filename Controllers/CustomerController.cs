using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // Получить всех клиентов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            if (customers == null || customers.Count == 0)
            {
                return NoContent();  // Возвращаем 204, если клиенты не найдены
            }
            return Ok(customers);  // Возвращаем 200 и список клиентов
        }

        // Получить клиента по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();  // Возвращаем 404, если клиент не найден
            }
            return Ok(customer);  // Возвращаем 200 и клиента
        }

        // Добавить нового клиента
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] CustomerDTO customerDTO)
        {
            var newCustomer = await _customerService.AddCustomer(customerDTO);
            return CreatedAtAction(nameof(GetCustomer), new { id = newCustomer.Id }, newCustomer);  // Возвращаем 201 и созданного клиента
        }

        // Обновить клиента
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, [FromBody] CustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("ID клиента не совпадает");  // Возвращаем 400, если ID не совпадают
            }

            var updatedCustomer = await _customerService.UpdateCustomer(customer);
            if (updatedCustomer == null)
            {
                return NotFound();  // Возвращаем 404, если клиент не найден
            }

            return Ok(updatedCustomer);  // Возвращаем 200 и обновленного клиента
        }

        // Удалить клиента
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteCustomer(id);
            if (!success)
            {
                return NotFound();  // Возвращаем 404, если клиент не найден
            }
            return Ok();  // Возвращаем 200 при успешном удалении
        }
    }




}
