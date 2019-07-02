using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
            new CityDto()
            {
                Id = 1,
                Name = "Manchester",
                Description = "Best city in the world!!",
                PointOfInterests = new List<PointOfInterestsDto>()
                {
                    new PointOfInterestsDto()
                    {
                        Id = 1,
                        Name = "Etihad",
                        Description = "Home of Manchester City Football club" },
                        new PointOfInterestsDto()
                        {
                            Id = 2,
                            Name = "Trafford Centre",
                            Description = "Shopping Centre" },
                        }
            },
            new CityDto()
            {
                Id = 2,
                Name = "Liverpool",
                Description = "Near Manchester" ,
            PointOfInterests = new List<PointOfInterestsDto>()
            {
            new PointOfInterestsDto()
            {
                Id = 1,
                Name = "Home of Liverpool and Everton Football club",
                Description = "Avoid both at all costs" },
            }
            },
            new CityDto()
            {
                Id = 3,
                Name = "Newcastle",
                Description = "The proper North",
                PointOfInterests = new List<PointOfInterestsDto>()
            {
            new PointOfInterestsDto()
            {
                Id = 1,
                Name = "Angel of the north ",
                Description = "it is a steel sculpture of an angel, 20 metres (66 ft) tall, with wings measuring 54 metres" },
            }
            },
            new CityDto()
            {
                Id = 4,
                Name = "London",
                Description = "UK Capital city",
                PointOfInterests = new List<PointOfInterestsDto>()
            {
            new PointOfInterestsDto()
            {
                Id = 1,
                Name = "BuckingHam Palace ",
                Description = "Home to the Queen, Our Elizabeth" },
            }
            },
          
        };

        }
    }

}