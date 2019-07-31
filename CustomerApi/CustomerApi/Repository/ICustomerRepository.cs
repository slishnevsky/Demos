using CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApi.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> FindAll();
    }
}