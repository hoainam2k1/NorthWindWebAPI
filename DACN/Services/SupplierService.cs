using System;
using System.Collections.Generic;
using DACN.Models;
using DACN.Data.Repositories;
namespace DACN.Services
{
    public interface ISupplierService
    {
        Supplier Add(Supplier supplier);
        void Update(int id, Supplier supplier);
        IEnumerable<Supplier> GetAll();
        IEnumerable<Supplier> GetListSupplierByName(string keyword);
        Supplier GetById(int id);
        Supplier Delete(int id);
    }
    public class SupplierService : ISupplierService
    {
        ISupplierRepository _supplierRepository;
        IProductService _productService;
        public SupplierService(ISupplierRepository supplierRepository, IProductService _productService)
        {
            this._supplierRepository = supplierRepository;
            this._productService = _productService;
        }
        public Supplier Add(Supplier supplier)
        {
            return _supplierRepository.Add(supplier);
        }
        public void Update(int id, Supplier supplier)
        {
            _supplierRepository.Update(id, supplier);
        }
        public IEnumerable<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }
        public Supplier GetById(int id)
        {
            return _supplierRepository.GetById(id);
        }
        public IEnumerable<Supplier> GetListSupplierByName(string keyword)
        {
            return _supplierRepository.GetListSupplierByName(keyword);
        }
        public Supplier Delete(int id)
        {
            var products = _productService.GetListProductBySupplier(id);
            foreach(var product in products)
            {
                int idd = product.ProductId;
                _productService.Delete(idd);
            }
            
            return _supplierRepository.Delete(id);
        }
    }
}
