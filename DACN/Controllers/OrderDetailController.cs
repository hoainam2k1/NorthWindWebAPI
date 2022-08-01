using DACN.Services;
using DACN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using DACN.ModelVM;
namespace DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        IOrderDetailService _orderdetailService;

        public OrderDetailController(IOrderDetailService orderdetailService)
        {
            this._orderdetailService = orderdetailService;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderdetailService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var orders = _orderdetailService.GetById(id);
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
        [Route("add/{id}")]
        public IActionResult Create(int id,OrderDetail[] orderdtVM)
        {
            try
            {
                
                    _orderdetailService.Add(id, orderdtVM);
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, OrderDetail orderdtVM)
        {
            try
            {
                _orderdetailService.Update(id, orderdtVM);
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
            var orders = _orderdetailService.Delete(id);
            return Ok(orders);
        }
    }
}
