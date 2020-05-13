using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kundeinformasjonstjenester.Models.Kundeinformasjon
{
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Screen> Screens { get; set; }
    }
}
