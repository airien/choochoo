using Kundeinformasjonstjenester.Models.ScreenHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kundeinformasjonstjenester.DataAccessLayer.ScreenHub
{
    public interface IScreenConnectionRepository
    {
        public ScreenConnection Get(ScreenUpdate update);
        public ScreenConnection Get(string connectionId);
        public IEnumerable<ScreenConnection> Get();
        public ScreenConnection Add(ScreenConnection station);
        public bool Delete(string id);
        void Replace(ScreenConnection connection);
    }
    public class ScreenConnectionRepository : IScreenConnectionRepository
    {
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
