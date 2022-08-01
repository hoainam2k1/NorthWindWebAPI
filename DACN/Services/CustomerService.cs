using DACN.Models;
using DACN.Data.Repositories;
using System.Collections.Generic;

namespace DACN.Services
{
    public interface ICustomerService
    {
        Customer Add(Customer customer);
        void Update(string id, Customer customer);
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetListCustomerByName(string keyword);

        Customer GetById(string id);
        Customer Delete(string id);
        Customer GetByUserName(string userName, string passWord);
    }
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _customerRepository;
        IOrderService _oderService;

        public CustomerService(ICustomerRepository customerRepository, IOrderService orderService)
        {
            this._customerRepository = customerRepository;
            this._oderService = orderService;
        }
        public Customer Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }
        public void Update(string id, Customer customer)
        {
            _customerRepository.Update(id, customer);
        }
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }
        public IEnumerable<Customer> GetListCustomerByName(string keyword)
        {
            return _customerRepository.GetListCustomerByName(keyword);
        }
        public Customer GetById(string id)
        {
            return _customerRepository.GetById(id);
        }
        public Customer Delete(string id)
        {
            var orders = _oderService.GetListOrderByCustomerName(id);
            foreach(var order in orders)
            {
                int idOD = order.OrderId;
                _oderService.Delete(idOD);
            }
            return _customerRepository.Delete(id);
        }
        public Customer GetByUserName(string userName, string passWord)
        {
            return _customerRepository.GetByUserName(userName, passWord);
        }
    }
}
