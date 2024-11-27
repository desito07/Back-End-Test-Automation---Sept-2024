using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPITesting
{
    public class WeatherForecast
    {

        public DateTime Date { get; set; } = DateTime.Now;

        //[JsonProperty("temperature_c")]
        public int TemperatureC { get; set; }

        public string Summary { get; set; } = "Hot summer days";
    }
}
