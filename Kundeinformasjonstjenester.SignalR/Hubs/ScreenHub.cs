using Kundeinformasjonstjenester.DataAccessLayer.ScreenHub;
using Kundeinformasjonstjenester.Models.ScreenHub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kundeinformasjonstjenester.SignalR.Hubs
{
    public class ScreenHub : Hub<IConnectionClient>
    {
        public ScreenHub(IScreenConnectionRepository repository)
        {
            Repository = repository;
        }

        public IScreenConnectionRepository Repository { get; }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var station = httpContext.Request.Query["station"];
            var track = httpContext.Request.Query["track"];
            var screen = httpContext.Request.Query["screen"];
            //TODO: sikkerhet
            ScreenConnection connection = new ScreenConnection
            {
                ConnectionId = Context.ConnectionId,
                Name = Context.User.Identity.Name,
                Timestamp = DateTime.Now.ToString("dd.MM.yyyyTHH:mm:ss"),
                Origin= Context.GetHttpContext().Request.Host.Value,
                Connected = true,
                StationId = station,
                TrackId = track,
                ScreenId = screen,
                Meta = "" // TODO: figure out if we need it
            };
            Repository.Add(connection);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Repository.Delete(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
