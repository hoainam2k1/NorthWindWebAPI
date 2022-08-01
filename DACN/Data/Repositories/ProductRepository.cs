using DACN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace DACN.Data.Repositories
{
    public interface IProductRepository
    {
        Product Add(Product Product);
        void Update(int id,Product Product);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetListProductByName(string keyword);
        IEnumerable<Product> GetProductBySupplier(int SupplierId);
        Product GetById(int id);
        Product Delete(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private NorthwindContext _context;
        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }
        public Product Add(Product Product)
        {
            var product = new Product
            {
                ProductName = Product.ProductName,
                SupplierId = Product.SupplierId,
                CategoryId = Product.CategoryId,
                QuantityPerUnit = Product.QuantityPerUnit,
                UnitPrice = Product.UnitPrice,
                UnitsInStock = Product.UnitsInStock,
                UnitsOnOrder = Product.UnitsOnOrder,
                ReorderLevel = Product.ReorderLevel,
                Discontinued = Product.Discontinued,
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public void Update(int id, Product Product)
        {
            var products = GetById(id);
            try
            {
                products.ProductName = Product.ProductName;
                products.SupplierId = Product.SupplierId;
                products.CategoryId = Product.CategoryId;
                products.QuantityPerUnit = Product.QuantityPerUnit;
                products.UnitPrice = Product.UnitPrice;
                products.UnitsInStock = Product.UnitsInStock;
                products.UnitsOnOrder = Product.UnitsOnOrder;
                products.ReorderLevel = Product.ReorderLevel;
                products.Discontinued = Product.Discontinued;
                _context.Products.Update(products);
                _context.SaveChanges();
            }
            catch
            {

            }
        }
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.AsNoTracking().ToList();
        }
        public IEnumerable<Product> GetListProductByName(string keyword)
        {
            return _context.Products.AsNoTracking().Where(x =>x.ProductName == keyword).ToList();
        }
        public IEnumerable<Product> GetProductBySupplier(int SupplierId)
        {
            return _context.Products.AsNoTracking().Include(x => x.Supplier).Where(x => x.SupplierId == SupplierId).ToList();
        }
        public Product Delete(int id)
        {
            var product = GetById(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }
        public Product GetById(int id)
        {
            return _context.Products.AsNoTracking().SingleOrDefault(p => p.ProductId == id);
        }
    }
}
