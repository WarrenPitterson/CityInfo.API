using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/Pointsofinterests")]
        public IActionResult GetPointsofInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c =>c.Id == cityId);
            
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointOfInterests);
        }

        [HttpGet("{cityId}/Pointsofinterests/{id}")]
        public IActionResult GetPointofInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterest = city.PointOfInterests.FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }
    }
}
