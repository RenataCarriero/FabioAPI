using FabioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabioAPI.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly Context _context;

        public OrdersController(Context context)
        {
            _context = context;
        }

        // GET: api/FetchOrders
        [HttpGet("FetchOrders")]
        public async Task<ActionResult<IEnumerable<OrderContract>>> FetchOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/GetOrder/5
        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderContract>> GetOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/UpdateOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(long id, OrderContract order)
        {
            if (id != order.id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CreateOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderContract>> CreateCustomer(OrderContract order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetOrder), new { id = order.id }, order);
        }

        // DELETE: api/DeleteOrder/5
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.id == id);
        }
    }
}
