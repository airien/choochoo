using Kundeinformasjonstjenester.Models.Kundeinformasjon;
using System.Collections.Generic;
using System.Linq;

namespace Kundeinformasjonstjenester.DataAccessLayer.Kundeinformasjon
{
    public interface IInformationRepository
    {
        public Information Get(string id);
        public IEnumerable<Information> Get();
        public Information Add(Information information);
        public bool Delete(string id);
    }
    public class InformationRepository : IInformationRepository
    {
        // TODO: implement db here
        private Dictionary<string, Information> Information { get; set; } = new Dictionary<string, Information>();

        public InformationRepository()
        {
            Information.Add("1", new Information
            {
                Id = "1",
                Src = "https://www.trains-worldexpresses.com/100/102x1_01m.jpg",
                Summary = "someData",
                Station = "Oslo",
                Track = "19",
                Screen = "1"
            });

            Information.Add("2", new Information
            {
                Id = "2",
                Src = "https://akm-img-a-in.tosshub.com/indiatoday/images/story/201504/a_maglev_train-650-x-418_042315112702.jpg",
                Summary = "someData",
                Station = "Oslo",
                Track = "19",
                Screen = "2"
            });
        }

        public Information Add(Information information)
        {
            if (information != null && !Information.ContainsKey(information.Id))
                Information.Add(information.Id, information);

            return information;
        }

        public bool Delete(string id)
        {
            if (id != null && Information.ContainsKey(id))
            {
                Information.Remove(id);
                return true;
            }
            return false;
        }

        public Information Get(string id)
        {
            return Information.GetValueOrDefault(id);
        }

        public IEnumerable<Information> Get()
        {
            return Information.Select(kvp => kvp.Value).ToList();
        }
    }
}
