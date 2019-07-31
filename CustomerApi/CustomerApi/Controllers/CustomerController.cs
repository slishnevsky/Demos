using CustomerApi.Models;
using CustomerApi.Repository;
using CustomerApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomerApi.Controllers
{
    [RoutePrefix("customers")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException("ICustomerService");
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var response = await _customerService.GetCustomersAsync();
            if (response.Success)
            {
                return Ok(response.Customers);
            }
            return InternalServerError(response.OperationException);
        }
    }
}
