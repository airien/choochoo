using System.Linq;
using Kundeinformasjonstjenester.DataAccessLayer.Kundeinformasjon;
using Kundeinformasjonstjenester.Models.Kundeinformasjon;
using Microsoft.AspNetCore.Mvc;

namespace Kundeinformasjonstjenester.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformasjonController : ControllerBase
    {
        public InformasjonController(IInformationRepository repository)
        {
            Repository = repository;
        }

        public IInformationRepository Repository { get; }

        [HttpGet]
        [Route("/api/information")]
        public Information Get(string id)
        {
            var info = Repository.Get(id);
            if (info == null)
            {
                info = Repository.Get().FirstOrDefault();
            }
            return info;
            // foreløpig bare mocke verdi
        }

        [HttpPost]
        [Route("/api/information")]
        public Information Get(Information information)
        {
            return Repository.Add(information);
            // foreløpig bare mocke verdi
        }
    }

}