using DACN.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using DACN.Data.Repositories;


namespace DACN.Services
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(int id,Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetListProductByName(string name);

        Product GetById(int id);
        IEnumerable<Product> GetListProductBySupplier(int supplierId);

    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        IOrderDetailRepository _orderDetailRepository;
        public ProductService(IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            this._productRepository = productRepository;  
            this._orderDetailRepository = orderDetailRepository;  
        }
        public Product Add(Product Product)
        {
            return _productRepository.Add(Product);
        }
        public void Update(int id,Product Product)
        {
            _productRepository.Update(id, Product);
        }
        public Product Delete(int id)
        {
            var orderDT = _orderDetailRepository.GetOrderDetailtByProduct(id);
            foreach(var orderdt in orderDT)
            {
                int ordId = orderdt.OrderId;
                _orderDetailRepository.Delete(ordId);
            }
            return _productRepository.Delete(id);
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public IEnumerable<Product> GetListProductByName(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))  return _productRepository.GetListProductByName(keyword);
            return _productRepository.GetAll();
        }
        public IEnumerable<Product> GetListProductBySupplier(int supplierId)
        {
            return _productRepository.GetProductBySupplier(supplierId);
        }
        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }
       
    }
}
