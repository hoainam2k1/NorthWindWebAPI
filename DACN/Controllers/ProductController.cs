using DACN.Services;
using DACN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<Product> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetById(int id)
        {
            return _productService.GetById(id);
            
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create(Product productvm)
        {
            try
            {
                var product = _productService.Add(productvm);
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(int id, Product productvm)
        {
            try
            {
                _productService.Update(id, productvm);
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
            var products = _productService.Delete(id);
            return Ok(products);
        }
    }
}