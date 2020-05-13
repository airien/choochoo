using System.Collections.Generic;
using Kundeinformasjonstjenester.DataAccessLayer.Kundeinformasjon;
using Kundeinformasjonstjenester.Models.Kundeinformasjon;
using Microsoft.AspNetCore.Mvc;

namespace Kundeinformasjonstjenester.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        public StationController(IStationRepository repository)
        {
            Repository = repository;
        }

        public IStationRepository Repository { get; }

        [HttpGet]
        [Route("/api/stations")]
        public IEnumerable<Station> Get()
        {
            return Repository.Get();
        }
    }
}