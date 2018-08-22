using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using MyStoreApi.Models;
using Newtonsoft.Json;

namespace MyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // Start the connection
        private readonly MyStoreContext _context;        
        
        public CustomerController(MyStoreContext context)
        {
            _context = context;
        }

        // GET api/Customer
        [HttpGet]
        public ActionResult<List<Customer>> GetAll( [FromServices]IDistributedCache cache)
        {
            string cacheCustomers = cache.GetString("customers");
            if (cacheCustomers == null)
            {
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                cacheCustomers = JsonConvert.SerializeObject(_context.customers);
                cache.SetString("customers", cacheCustomers, cacheOptions);
                
            }
            return Content(cacheCustomers, "application/json");

        }

        // GET api/Customer{id}
        [HttpGet("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> GetbyId(long id)
        {
            var item = _context.customers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // POST api/Customer
        [HttpPost]
        public IActionResult Create(Customer item)
        {
            _context.customers.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomer", new{id = item.customerID},item);
        }

        // PUT api/Customer
        [HttpPut("{id}")]
        public IActionResult Update(long id, Customer item)
        {
            var customer = _context.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.name = item.name;
            customer.address = item.address;
            customer.contact = item.contact;

            _context.customers.Update(customer);
            _context.SaveChanges();
            return NoContent();

        }

        //DELETE api/Customer
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var customer = _context.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }

    }
}