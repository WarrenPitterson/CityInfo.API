﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest { get {
                return PointOfInterests.Count;
            }
        }

        public ICollection<PointOfInterestsDto> PointOfInterests { get; set; } = new List<PointOfInterestsDto>();

    }
}
