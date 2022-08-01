using DACN.Services;
using DACN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
namespace DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<Order> GetAll()
        {
            return _orderService.GetAll();
        }
        [HttpGet]
        public IEnumerable<Order> GetByCus(string id)
        {
            return _orderService.GetListOrderByCustomerName(id);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var orders = _orderService.GetById(id);
            if (orders != null)
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Create(Order orderVM)
        {
            try
            {
                var order = _orderService.Add(orderVM);
                return Ok(order);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, Order orderVM)
        {
            try
            {
                _orderService.Update(id, orderVM);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderService.Delete(id);
            return Ok(order);
        }
    }
}
