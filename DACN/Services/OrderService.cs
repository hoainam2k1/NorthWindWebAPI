using DACN.Models;
using DACN.Data.Repositories;
using System.Collections.Generic;
namespace DACN.Services
{
    public interface IOrderService
    {
        Order Add(Order order);
        void Update(int id, Order order);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetListOrderByCustomerName(string keyword);
        Order GetById(int id);
        Order Delete(int id);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
        }
        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }
        public void Update(int id, Order order)
        {
            _orderRepository.Update(id, order);
        }
        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }
        public IEnumerable<Order> GetListOrderByCustomerName(string keyword)
        {
            return _orderRepository.GetListOrderByCustomerName(keyword);
        }
        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }
        public Order Delete(int id)
        {
            var orderDetail = _orderDetailRepository.GetById(id);
            foreach(var order in orderDetail)
            {
                _orderDetailRepository.Delete(order.OrderId);
            }
            return _orderRepository.Delete(id);
        }
        
    }
}
