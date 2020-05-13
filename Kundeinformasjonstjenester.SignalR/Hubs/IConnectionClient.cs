using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kundeinformasjonstjenester.SignalR.Hubs
{
    public interface IConnectionClient
    {
        public Task ConnectToUrl(string url);
    }
}
