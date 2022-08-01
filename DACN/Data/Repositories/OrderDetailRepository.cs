using DACN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DACN.Data.Repositories
{
    public interface IOrderDetailRepository
    {
        OrderDetail Add(int id,OrderDetail[] orderdetail);
        void Update(int id, OrderDetail orderdetail);
        IEnumerable<OrderDetail> GetAll();
        IEnumerable<OrderDetail> GetById(int id);
        IEnumerable<OrderDetail> GetOrderDetailtByProduct(int productId);
        bool Delete(int id);
    }
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private NorthwindContext _context;
        public OrderDetailRepository(NorthwindContext context)
        {
            _context = context;
        }
        public OrderDetail Add(int id,OrderDetail[] orderdetail)
        {
            var orderDetail = new OrderDetail();
            try
            {
                

                foreach (var item in orderdetail)
                {
                    orderDetail.OrderId = id;
                    orderDetail.ProductId = item.ProductId;
                    orderDetail.UnitPrice = item.UnitPrice;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Discount = item.Discount;
                    _context.OrderDetails.Add(orderDetail);
                    _context.SaveChanges();
                }

                return orderDetail;
            }
            catch (Exception ex)
            {
                return orderDetail;
            }
            
        }
        public void Update(int id, OrderDetail orderdetail)
        {
            var orderDetail = GetById(id);
            try
            {
                foreach(var orderdt in orderDetail)
                {
                    orderdt.ProductId = orderdetail.ProductId;
                    orderdt.UnitPrice = orderdetail.UnitPrice;
                    orderdt.Quantity = orderdetail.Quantity;
                    orderdt.Discount = orderdetail.Discount;
                    _context.OrderDetails.Update(orderdt);
                    _context.SaveChanges();
                }
            }
            catch
            {

            }
        }
        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.AsNoTracking().ToList();
        }
        public IEnumerable<OrderDetail> GetById(int id)
        {
            return _context.OrderDetails.AsNoTracking().Where(p => p.OrderId == id).ToList();
        }
        public IEnumerable<OrderDetail> GetOrderDetailtByProduct(int productId)
        {
            return _context.OrderDetails.AsNoTracking().Include(x => x.Product).Where(x => x.ProductId == productId).ToList();
        }
        public bool Delete(int id)
        {
            var orderDT = GetById(id);
            foreach(var order in orderDT)
            {
                _context.OrderDetails.Remove(order);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
