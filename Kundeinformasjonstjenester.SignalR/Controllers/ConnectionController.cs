using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kundeinformasjonstjenester.DataAccessLayer.ScreenHub;
using Kundeinformasjonstjenester.Models.ScreenHub;
using Kundeinformasjonstjenester.SignalR.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Kundeinformasjonstjenester.SignalR.Controllers
{
    //TODO: implement authentication + authorization
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        public ConnectionController(IScreenConnectionRepository repository, IHubContext<ScreenHub, IConnectionClient> hubcontext)
        {
            Repository = repository;
            HubContext = hubcontext;
        }

        public IScreenConnectionRepository Repository { get; }
        public IHubContext<ScreenHub, IConnectionClient> HubContext { get; }

        [HttpGet]
        public IEnumerable<ScreenConnection> Get()
        {
            return Repository.Get();
        }
        
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> PushUrl(ScreenUpdate update)
        {
            var connection = Repository.Get(update);
            if(connection != null)
            {
                await HubContext.Clients.Client(connection.ConnectionId).ConnectToUrl(update.Url);
                return Ok("Message Sent");
            }
            else
            {
                return NotFound("Connection not found");
            }

        }
    }
}