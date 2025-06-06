using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataPostgresApi.Data;
using ODataPostgresApi.Models;

namespace ODataPostgresApi.Controllers
{
    [Route("odata/Products")]

    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Products);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public IActionResult Get(int key)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == key);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Created(product);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put(int key, [FromBody] Product update)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null)
                return NotFound();

            product.Name = update.Name;
            product.Price = update.Price;

            await _context.SaveChangesAsync();
            return Updated(product);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
