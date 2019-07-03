using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{cityId}/Pointsofinterests")]
        public IActionResult GetPointsofInterests(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c =>c.Id == cityId);
            
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterests);
        }

        [HttpGet("{cityId}/Pointsofinterests/{id}")]
        public IActionResult GetPointsofInterests(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            var pointsOfInterests = city.PointsOfInterests.FirstOrDefault(p => p.Id == id);

            if (pointsOfInterests == null)
            {
                return NotFound();
            }
            return Ok(pointsOfInterests);
        }
    }
}
