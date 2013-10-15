using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PhindAPI.Models
{
    public class PhotoModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string LargeUrl { get; set; }

        public string Small320Url { get; set; }

        public string LargeSquareThumbnailUrl { get; set; }
    }
}