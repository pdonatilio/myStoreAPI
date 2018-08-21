using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MyStoreApi.Models;

namespace MyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Start the connection
        private readonly MyStoreContext _context;        
        
        public ProductController(MyStoreContext context)
        {
            _context = context;
        }

        // GET api/Product
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _context.products.ToList();
        }
        
        // GET api/Product{id}
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> GetbyId(long id)
        {
            var item = _context.products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/Product
        [HttpPost]
        public IActionResult Create(Product item)
        {
            _context.products.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new{id = item.productID},item);
        }

        // PUT api/Product
        [HttpPut("{id}")]
        public IActionResult Update(long id, Product item)
        {
            var product = _context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.description = item.description;
            product.unitInStock = item.unitInStock;
            product.unitPrice = item.unitPrice;

            _context.products.Update(product);
            _context.SaveChanges();
            return NoContent();

        }

        //DELETE api/Product
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var product = _context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

    }

}