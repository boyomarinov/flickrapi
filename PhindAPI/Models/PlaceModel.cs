using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhindAPI.Models
{
    public class PlaceModel
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public int? PhotoCount { get; set; }

        public string Timezone { get; set; }
    }
}