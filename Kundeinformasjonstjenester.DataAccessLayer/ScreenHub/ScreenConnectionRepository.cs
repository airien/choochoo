using Kundeinformasjonstjenester.Models.ScreenHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Kundeinformasjonstjenester.DataAccessLayer.ScreenHub
{
    public interface IScreenConnectionRepository
    {
        ScreenConnection Get(ScreenUpdate update);
        ScreenConnection Get(string connectionId);
        IEnumerable<ScreenConnection> Get();
        public ScreenConnection Add(ScreenConnection station);
        bool Delete(string id);
        void Replace(ScreenConnection connection);
        Task<string> FetchUpdateAsync(ScreenUpdate update);
    }
    public class ScreenConnectionRepository : IScreenConnectionRepository
    {
        public IConnectionService Service { get; }

        public ScreenConnectionRepository(IConnectionService service)
        {
            Service = service;
        }

        //TODO: Implement Redis
        private Dictionary<string, ScreenConnection> Connections { get; set; } = new Dictionary<string, ScreenConnection>();
        public ScreenConnection Add(ScreenConnection connection)
        {
            if (Connections.ContainsKey(connection.ConnectionId))
            {
                Connections[connection.ConnectionId] = connection;
            }
            else
            {
                Connections.Add(connection.ConnectionId, connection);
            }
            return connection;
        }

        public bool Delete(string id)
        {
            if (id != null && Connections.ContainsKey(id))
            {
                Connections.Remove(id);
                return true;
            }
            return false;
        }

        public async Task<string> FetchUpdateAsync(ScreenUpdate update)
        {
            return await Service.FetchUrlAsync(update.Url);
        }

        public ScreenConnection Get(string connectionId)
        {

            return Connections.GetValueOrDefault(connectionId);
        }

        public IEnumerable<ScreenConnection> Get()
        {
            return Connections.Select(kvp => kvp.Value).ToList();
        }

        public ScreenConnection Get(ScreenUpdate update)
        {
            return Connections.Select(kvp => kvp.Value).Where(x => x.StationId == update.StationId && x.TrackId == update.TrackId && x.ScreenId == update.ScreenId).FirstOrDefault();
        }

        public void Replace(ScreenConnection connection)
        {
            if (Connections.ContainsKey(connection.ConnectionId))
            {
                Connections[connection.ConnectionId] = connection;
            }
        }
    }
}
