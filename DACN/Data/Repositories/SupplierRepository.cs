using DACN.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace DACN.Data.Repositories
{
    public interface ISupplierRepository
    {
        Supplier Add(Supplier supplier);
        void Update(int id, Supplier supplier);
        IEnumerable<Supplier> GetAll();
        IEnumerable<Supplier> GetListSupplierByName(string keyword);
        Supplier GetById(int id);
        Supplier Delete(int id);
    }
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext _context;
        public SupplierRepository(NorthwindContext context)
        {
            _context = context;
        }
        public Supplier Add(Supplier supplier)
        {
            var Supplier = new Supplier
            {
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                Country = supplier.Country,
                PostalCode = supplier.PostalCode,
                Phone = supplier.Phone,
                Fax = supplier.Fax
            };
            _context.Suppliers.Add(Supplier);
            _context.SaveChanges();
            return Supplier;
        }
        public void Update(int id, Supplier supplier)
        {
            var sup = GetById(id);
            try
            {
                sup.CompanyName = supplier.CompanyName;
                sup.ContactName = supplier.ContactName;
                sup.ContactTitle = supplier.ContactTitle;
                sup.Address = supplier.Address;
                sup.City = supplier.City;
                sup.Region = supplier.Region;
                sup.Country = supplier.Country;
                sup.PostalCode = supplier.PostalCode;
                sup.Phone = supplier.Phone;
                sup.Fax = supplier.Fax;
                _context.Suppliers.Update(sup);
                _context.SaveChanges();
            }
            catch
            {

            }
        }
        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers.AsNoTracking().ToList();
        }
        public Supplier GetById(int id)
        {
            return _context.Suppliers.AsNoTracking().SingleOrDefault(p => p.SupplierId == id);
        }
        public IEnumerable<Supplier> GetListSupplierByName(string keyword)
        {
            return _context.Suppliers.AsNoTracking().Where(x => x.CompanyName == keyword).ToList();
        }
        public Supplier Delete(int id)
        {
            var supplier = GetById(id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return supplier;
        }
    }
}
