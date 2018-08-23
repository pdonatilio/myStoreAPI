using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using MyStoreApi.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // Start the connection
        private readonly MyStoreContext _context;        
        
        public OrderController(MyStoreContext context)
        {
            _context = context;
        }

        // GET api/Order
        [HttpGet]       
        public ActionResult<List<Order>> GetAll([FromServices]IDistributedCache cache)
        {
            string cacheOrders = cache.GetString("order");
            if (cacheOrders == null)
            {
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                cacheOrders = JsonConvert.SerializeObject(_context.orders.Include(c => c.customers).ToList());
                cache.SetString("order", cacheOrders, cacheOptions);
            }
            return Content(cacheOrders, "application/json");
        }
        
        // GET api/Order{id}
        [HttpGet("{id}", Name = "GetOrder")]
        public ActionResult<Order> GetbyId(long id)
        {
            var item = _context.orders.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            //Not optimized, need to be improved
            item.customers = _context.customers.Find(item.customerID);
            return item;
        }

        // POST api/Order
        [HttpPost]
        public IActionResult Create(Order item)
        {
            item.customers = _context.customers.Find(item.customerID);
            _context.orders.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetOrder", new{id = item.orderID},item);
        }

        // PUT api/Order
        [HttpPut("{id}")]
        public IActionResult Update(long id, Order item)
        {
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            order.customerID = item.customerID;
            order.invoice = item.invoice;

            _context.orders.Update(order);
            _context.SaveChanges();
            return NoContent();

        }

        //DELETE api/Order/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }

    }

}