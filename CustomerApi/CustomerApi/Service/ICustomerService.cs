using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerApi.Service
{
    public interface ICustomerService
    {
        Task<GetCustomersResponse> GetCustomersAsync();
    }
}