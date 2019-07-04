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



        [HttpGet("{cityId}/Pointsofinterest")]
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

                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem has been encountered with your request");
            }

        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetOnePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointsOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointsOfInterest);
        }

    



[HttpPost("{cityId}/pointsofinterest")]
public IActionResult CreatePointsOfInterest(int cityId, [FromBody] PointsOfInterestForCreationDto pointsOfInterest)
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

    var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
    var finalPointOfInterest = new PointsOfInterestDto()
    {
        Id = ++maxPointOfInterestId,
        Name = pointsOfInterest.Name,
        Description = pointsOfInterest.Description
    };
    city.PointsOfInterest.Add(finalPointOfInterest);

    return CreatedAtRoute("GetPointOfInterest", new
    { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
}


[HttpPut("{cityId}/pointsOfinterest/{id}")]
public IActionResult UpdatePointsOfinterest(int cityId, int id,
    [FromBody] PointsOfInterestForUpdateDto pointsOfInterest)
{
    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

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

    if (city == null)
            {
                return NotFound();
            }

    var pointsOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);

    if (pointsOfInterestFromStore == null)
    {
        return NotFound();
    }

    pointsOfInterestFromStore.Name = pointsOfInterest.Name;
    pointsOfInterestFromStore.Description = pointsOfInterest.Description;

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

    var pointsOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
    if (pointsOfInterestFromStore == null)
    {
        return NotFound();
    }

    city.PointsOfInterest.Remove(pointsOfInterestFromStore);

    return NoContent();

}
    }
}



