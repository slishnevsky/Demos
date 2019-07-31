using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularSignalRDemo.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AngularSignalRDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public HomeController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "WebApi", "Hello from WebApi");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(string name, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", name, message);
            return Ok();
        }
    }
}
