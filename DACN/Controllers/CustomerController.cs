using DACN.Services;
using DACN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
namespace DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        [HttpGet]
        [Route("getall")]
        public IEnumerable<Customer> GetAll()
        {
            return _customerService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var customer = _customerService.GetById(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("login")]
        public IActionResult login(string userName, string passWord)
        {
            var customer = _customerService.GetByUserName(userName, passWord);
            if(customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Create(Customer customerVM)
        {
            try
            {
                var cus = _customerService.Add(customerVM);
                return Ok(cus);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(string id, Customer customervm)
        {
            try
            {
                _customerService.Update(id, customervm);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var products = _customerService.Delete(id);
            return Ok(products);
        }
    }
}
