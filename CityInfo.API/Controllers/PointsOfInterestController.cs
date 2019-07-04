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
        //private LocalMailService _mailservice;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
            //_mailservice = mailservice;
        }



        [HttpGet("{cityId}/Pointsofinterests")]
        public IActionResult GetPointsofInterest(int cityId)
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

        [HttpGet("{cityId}/pointsofinterests/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetOnePointOfInterest(int cityId, int id)
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

    



[HttpPost("{cityId}/pointsofinterests")]
public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointsOfInterestsForCreationDto pointsOfInterests)
{
    if (pointsOfInterests == null)
    {
        return BadRequest();
    }

    if (pointsOfInterests.Description == pointsOfInterests.Name)
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
        Name = pointsOfInterests.Name,
        Description = pointsOfInterests.Description
    };
    city.PointsOfInterests.Add(finalPointOfInterest);

    return CreatedAtRoute("GetPointOfInterest", new
    { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
}


[HttpPut("{cityId}/pointsofinterest/{id}")]
public IActionResult UpdatePointOfinterest(int cityId, int id,
    [FromBody] PointsOfInterestForUpdateDto pointsOfInterests)
{
    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

    if (pointsOfInterests == null)
    {
        return BadRequest();
    }

    if (pointsOfInterests.Description == pointsOfInterests.Name)
    {
        ModelState.AddModelError("Description", "The provide description should be different from the name");
    }

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == id);

    if (pointOfInterestFromStore == null)
    {
        return NotFound();
    }

    pointOfInterestFromStore.Name = pointsOfInterests.Name;
    pointOfInterestFromStore.Description = pointsOfInterests.Description;

    return NoContent();
}

[HttpDelete("{cityId}/pointsofinterest/{id}")]
public IActionResult DeletePointOfInterest(int cityId, int id)
{
    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
    if (city == null)
    {
        return NotFound();
    }

    var pointOfInterestFromStore = city.PointsOfInterests.FirstOrDefault(c => c.Id == id);
    if (pointOfInterestFromStore == null)
    {
        return NotFound();
    }

    city.PointsOfInterests.Remove(pointOfInterestFromStore);

    return NoContent();

}
    }
}



