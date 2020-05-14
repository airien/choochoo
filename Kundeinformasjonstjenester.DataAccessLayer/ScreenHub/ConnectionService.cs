using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kundeinformasjonstjenester.DataAccessLayer.ScreenHub
{
    public interface IConnectionService
    {
        public Task<string> FetchUrlAsync(string url);
    }
    public class ConnectionService : IConnectionService
    {
        public HttpClient Client { get; }
        public ConnectionService(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> FetchUrlAsync(string url)
        {
            var res = await Client.GetAsync(url);
            var str = await res.Content.ReadAsStringAsync();
            return str;
        }
    }
}
