using System;
using System.Collections.Generic;
using System.Text;

namespace Kundeinformasjonstjenester.Models.ScreenHub
{
    public class ScreenConnection
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Timestamp { get; set; }
        public string Origin { get; set; }
        public string StationId { get; set; }
        public string TrackId { get; set; }
        public string ScreenId { get; set; }
        public bool Connected { get; set; }
        public string Meta { get; set; }
    }
}
