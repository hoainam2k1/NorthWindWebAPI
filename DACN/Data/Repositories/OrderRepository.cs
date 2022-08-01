using DACN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DACN.Data.Repositories
{
    public interface IOrderRepository
    {
        Order Add(Order order);
        void Update(int id, Order order);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetListOrderByCustomerName(string keyword);
        Order GetById(int id);
        Order Delete(int id);
        IEnumerable<Order> GetOrdertByDetail(int orderId);
    }
    public class OrderRepository : IOrderRepository
    {
        private NorthwindContext _context;
        public OrderRepository(NorthwindContext context)
        {
            _context = context;
        }
        public Order Add(Order order)
        {
            var orders = new Order
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                Freight = order.Freight,
                ShipAddress = order.ShipAddress,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
            };
            _context.Orders.Add(orders);
            _context.SaveChanges();
            return orders;
        }
        public void Update(int id, Order order)
        {
            var orders = GetById(id);
            try
            {
                orders.Customer = order.Customer;
                orders.Employee = order.Employee;
                orders.OrderDate = order.OrderDate;
                orders.RequiredDate = order.RequiredDate;
                orders.Freight = order.Freight;
                orders.ShipAddress = order.ShipAddress;
                orders.ShipPostalCode = order.ShipPostalCode;
                orders.ShipCountry = order.ShipCountry;
                _context.Orders.Update(orders);
                _context.SaveChanges();
            }
            catch
            {

            }
        }
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.AsNoTracking().ToList();
        }
        public IEnumerable<Order> GetListOrderByCustomerName(string keyword)
        {
            return _context.Orders.AsNoTracking().Where(x => x.CustomerId == keyword).ToList();
        }
        public Order GetById(int id)
        {
            return _context.Orders.AsNoTracking().SingleOrDefault(p => p.OrderId == id);
        }
        public IEnumerable<Order> GetOrdertByDetail(int orderId)
        {
            return _context.Orders.AsNoTracking().Include(x => x.OrderDetails).Where(x => x.OrderId == orderId).ToList();
        }
        public Order Delete(int id)
        {
            var order = GetById(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return order;
        }
    }
}
