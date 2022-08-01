using DACN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DACN.Data.Repositories
{
    public interface ICustomerRepository
    {
        Customer Add(Customer customer);
        void Update(string id, Customer customer);
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetListCustomerByName(string keyword);
        Customer GetByUserName(string userName,string passWord);
        Customer GetById(string id);
        Customer Delete(string id);

    }
    public class CustomerRepository : ICustomerRepository
    {
        private NorthwindContext _context;
        public CustomerRepository(NorthwindContext context)
        {
            _context = context;
        }
        public Customer Add(Customer customer)
        { 
            var customers = new Customer
            {
                CustomerId = customer.CustomerId.Replace(" ", ""),
                PassWord = customer.PassWord,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Fax = customer.Fax,
                
        };
            
            _context.Customers.Add(customers);
            _context.SaveChanges();
            return customers;
        }
        public void Update(string id, Customer customer)
        {
            var customers = GetById(id);
            try
            {
                customers.PassWord = customer.PassWord;
                customers.CompanyName = customer.CompanyName;
                customers.ContactName = customer.ContactName;
                customers.ContactTitle = customer.ContactTitle;
                customers.Address = customer.Address;
                customers.City = customer.City;
                customers.Region = customer.Region;
                customers.PostalCode = customer.PostalCode;
                customers.Country = customer.Country;
                customers.Phone = customer.Phone;
                customers.Fax = customer.Fax;
                _context.Customers.Update(customers);
                _context.SaveChanges();
            }
            catch
            {

            }
        }
        public Customer GetByUserName(string userName,string passWord)
        {
            return _context.Customers.AsNoTracking().SingleOrDefault(x=> x.CustomerId == userName && x.PassWord == passWord);
        }
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.AsNoTracking().ToList();
        }
        public IEnumerable<Customer> GetListCustomerByName(string keyword)
        {
            return _context.Customers.AsNoTracking().Where(x => x.ContactName == keyword).ToList();
        }
        public Customer GetById(string id)
        {
            return _context.Customers.AsNoTracking().SingleOrDefault(p => p.CustomerId == id);
        }
        public Customer Delete(string id)
        {
            var customer = GetById(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer;
        }
       
    }
}
