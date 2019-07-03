using CityInfo.API.Models;
using CityInfo.API.Services;
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
        private LocalMailService _mailservice;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
            //_mailservice = mailservice;
        }



        [HttpGet("{cityId}/Pointsofinterests/{id}", Name = "GetPointofInterest")]
        public IActionResult GetPointsofInterests(int cityId, int id)
        {
            try
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest");
                    return NotFound();
                }

                return Ok(city.PointsOfInterests);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem has been encountered with your request");
            }
            
        }

        [HttpPost("{cityId}/pointsofinterests")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointsOfInterestsForCreationDto pointsOfInterest)
        {
            if (pointsOfInterest == null)
            {
                return BadRequest();
            }

            if (pointsOfInterest.Description == pointsOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provide description should be different from the name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterests).Max(p => p.Id);


            var finalPointOfInterest = new PointsOfInterestsDto()
            {
                Id = +maxPointOfInterestId,
                Name = pointsOfInterest.Name,
                Description = pointsOfInterest.Description
            };
            city.PointsOfInterests.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
            }
        }

    }

