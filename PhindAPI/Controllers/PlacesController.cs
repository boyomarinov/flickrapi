using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlickrNet;
using PhindAPI.Models;

namespace PhindAPI.Controllers
{
    public class PlacesController : BaseApiController
    {
        public Flickr Context { get; set; }

        public PlacesController()
        {
            this.Context = new Flickr("0c9642199bdc3afa4bc037439fe09c71", "e62b33c2ea0a4f93");
        }

        [HttpGet]
        [ActionName("top")]
        public IEnumerable<PlaceModel> GetTopPlaces(string type)
        {
            return this.ExecuteOperationAndHandleExceptions(() =>
            {
                PlaceType placeType = new PlaceType();
                switch (type)
                {
                    case "Continent":
                        placeType = PlaceType.Continent;
                        break;
                    case "Country":
                        placeType = PlaceType.Country;
                        break;
                    case "Region":
                        placeType = PlaceType.Region;
                        break;
                    case "Locality":
                        placeType = PlaceType.Locality;
                        break;
                    default:
                        placeType = PlaceType.Country;
                        break;
                }

                var places = this.Context.PlacesGetTopPlacesList(placeType)
                                 .Select(x => new PlaceModel
                                 {
                                     Id = x.PlaceId,
                                     Description = x.Description,
                                     Latitude = x.Latitude,
                                     Longitude = x.Longitude,
                                     PhotoCount = x.PhotoCount,
                                     Timezone = x.TimeZone
                                 });

                return places;
            });
        }
    }
}
