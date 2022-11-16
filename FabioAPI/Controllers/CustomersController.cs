using FabioApi.Models;
using FabioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FabioAPI.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        // GET: api/FetchCustomers
        [HttpGet("FetchCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerContract>>> FetchCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/GetCustomer/5
        [HttpGet("GetCustomer")]
        public async Task<ActionResult<CustomerContract>> GetCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST: api/UpdateCustomer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(long id, CustomerContract customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/CreateCustomer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<CustomerContract>> CreateCustomer(CustomerContract customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.id }, customer);
        }

        // DELETE: api/DeleteCustomer/5
        [HttpDelete("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.id == id);
        }
    }
}

