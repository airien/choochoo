using Kundeinformasjonstjenester.Models.Kundeinformasjon;
using System.Collections.Generic;
using System.Linq;

namespace Kundeinformasjonstjenester.DataAccessLayer.Kundeinformasjon
{
    public interface IStationRepository
    {
        public Station Get(string id);
        public IEnumerable<Station> Get();
        public Station Add(Station station);
        public bool Delete(string id);
    }
    public class StationRepository : IStationRepository
    {
        // TODO: implement db here
        private Dictionary<string, Station> Stations { get; set; } = new Dictionary<string, Station>();

        public StationRepository()
        {
            Stations.Add("1", new Station
            {
                Id = "1",
                Name = "Oslo S",
                Tracks = new List<Track> { new Track { Id = "1", Name = "1", Screens = new List<Screen> {new Screen{Id="AB",Name="SkjermAB" } } },
                new Track { Id = "2", Name = "2" , Screens = new List<Screen> {new Screen{Id="savadfa",Name="Skjermsavadfa" } } },
                new Track { Id = "3", Name = "3", Screens = new List<Screen> {new Screen{Id= "sadav", Name="Skjermsadav" } }  },
                new Track { Id = "4", Name = "4" , Screens = new List<Screen> {new Screen{Id= "fdsb", Name="Skjermfdsb" } } },
                new Track { Id = "5", Name = "5" , Screens = new List<Screen> {new Screen{Id= "bsbr", Name="Skjermbsbr" } } },
                new Track { Id = "6", Name = "6", Screens = new List<Screen> {new Screen{Id= "sdas", Name="Skjermasdas" } }  },
                new Track { Id = "7", Name = "7", Screens = new List<Screen> {new Screen{Id= "dsfbs", Name="Skjermdsfbs" } }  },
                new Track { Id = "8", Name = "8", Screens = new List<Screen> {new Screen{Id= "ewqcs", Name="Skjermewqcs" } }  },
                new Track { Id = "9", Name = "9", Screens = new List<Screen> {new Screen{Id= "wqewqer", Name="Skjermwqewqer" } }  },
                new Track { Id = "10", Name = "10", Screens = new List<Screen> {new Screen{Id= "saddsad", Name="Skjermsaddsad" }, new Screen { Id = "asgref", Name = "Skjermasgref" } }  }}
            });

            Stations.Add("2", new Station
            {
                Id = "2",
                Name = "Lillestrøm",
                Tracks = new List<Track> { new Track { Id = "1", Name = "1", Screens = new List<Screen> {new Screen{Id="AB",Name="SkjermAB" } } },
                new Track { Id = "2", Name = "2" , Screens = new List<Screen> {new Screen{Id="savadfa",Name="Skjermsavadfa" } } },
                new Track { Id = "3", Name = "3", Screens = new List<Screen> {new Screen{Id= "sadav", Name="Skjermsadav" } }  },
                new Track { Id = "4", Name = "4" , Screens = new List<Screen> {new Screen{Id= "fdsb", Name="Skjermfdsb" } } },
                new Track { Id = "5", Name = "5" , Screens = new List<Screen> {new Screen{Id= "bsbr", Name="Skjermbsbr" } } },
                new Track { Id = "6", Name = "6", Screens = new List<Screen> {new Screen{Id= "sdas", Name="Skjermasdas" } }  },
                new Track { Id = "7", Name = "7", Screens = new List<Screen> {new Screen{Id= "dsfbs", Name="Skjermdsfbs" } }  },
                new Track { Id = "8", Name = "8", Screens = new List<Screen> {new Screen{Id= "ewqcs", Name="Skjermewqcs" } }  },
                new Track { Id = "9", Name = "9", Screens = new List<Screen> {new Screen{Id= "wqewqer", Name="Skjermwqewqer" } }  },
                new Track { Id = "10", Name = "10", Screens = new List<Screen> {new Screen{Id= "saddsad", Name="Skjermsaddsad" }, new Screen { Id = "asgref", Name = "Skjermasgref" } }  }}
            });
        }

        public Station Add(Station station)
        {
            if (station != null && !Stations.ContainsKey(station.Id))
                Stations.Add(station.Id, station);

            return station;
        }

        public bool Delete(string id)
        {
            if (id != null && Stations.ContainsKey(id))
            {
                Stations.Remove(id);
                return true;
            }
            return false;
        }

        public Station Get(string id)
        {
            return Stations.GetValueOrDefault(id);
        }

        public IEnumerable<Station> Get()
        {
            return Stations.Select(kvp => kvp.Value).ToList();
        }
    }
}
