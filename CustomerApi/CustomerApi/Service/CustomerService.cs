using CustomerApi.Models;
using CustomerApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerApi.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException("Customer repository!");
        }

        public async Task<GetCustomersResponse> GetCustomersAsync()
        {
            return await Task.Run(() => GetCustomers());
        }

        private GetCustomersResponse GetCustomers()
        {
            GetCustomersResponse response = new GetCustomersResponse();
            try
            {
                IEnumerable<Customer> customers = _customerRepository.FindAll();
                response.Customers = customers;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.OperationException = ex;
            }
            return response;
        }
    }
}