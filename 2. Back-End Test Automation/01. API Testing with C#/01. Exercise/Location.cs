using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoNunitTest
{
    internal class Location
    {
        [JsonProperty("post code")]
        public string PostCode {  get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public List<Place> Places { get; set; }
    }
}
