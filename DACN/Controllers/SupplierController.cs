using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DACN.Models;
using System.Collections.Generic;
using DACN.Services;
namespace DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }
        [HttpGet]
        [Route("getall")]
        public IEnumerable<Supplier> GetAll()
        {
            return _supplierService.GetAll();
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var supplier = _supplierService.GetById(id);
            if (supplier != null)
            {
                return Ok(supplier);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Create(Supplier supplierVM)
        {
            try
            { 
                var sup = _supplierService.Add(supplierVM);
                return Ok(sup);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, Supplier supplierVM)
        {
            try
            {
                _supplierService.Update(id,supplierVM);
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
            var products = _supplierService.Delete(id);
            return Ok(products);
        }
    }
}
