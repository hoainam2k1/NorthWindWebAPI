using DACN.Models;
using DACN.Data.Repositories;
using System.Collections.Generic; 
namespace DACN.Services
{
    public interface IOrderDetailService
    {
        OrderDetail Add(int id,OrderDetail[] orderdetail);
        void Update(int id, OrderDetail orderdetail);
        IEnumerable<OrderDetail> GetAll();
        IEnumerable<OrderDetail> GetById(int id);
        bool Delete(int id);
    }
    public class OrderDetailService : IOrderDetailService
    {
        IOrderDetailRepository _orderdetailRepository;
        IOrderRepository _orderRepository;

        public OrderDetailService(IOrderDetailRepository orderdetailRepository, IOrderRepository orderRepository)
        {
            this._orderdetailRepository = orderdetailRepository;
            this._orderRepository = orderRepository;
        }
        public OrderDetail Add(int id,OrderDetail[] orderdetail)
        {
            return _orderdetailRepository.Add(id,orderdetail);
        }
        public void Update(int id, OrderDetail orderdetail)
        {
            _orderdetailRepository.Update(id, orderdetail);
        }
        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderdetailRepository.GetAll();
        }
        public IEnumerable<OrderDetail> GetById(int id)
        {
            return _orderdetailRepository.GetById(id);
        }
        public bool Delete(int id)
        {
            var orders = _orderRepository.GetOrdertByDetail(id);
            foreach (var order in orders)
            {
                int orderid = order.OrderId;
                _orderRepository.Delete(orderid);
            }
            return true;
        }
    }
}
