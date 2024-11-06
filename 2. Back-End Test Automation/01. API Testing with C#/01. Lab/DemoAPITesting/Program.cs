using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace DemoAPITesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //built-in system.text.json nugget package
            WeatherForecast forecast = new WeatherForecast();

            string weatherInfo = System.Text.Json.JsonSerializer.Serialize(forecast);

            Console.WriteLine(weatherInfo);

            string jsonString = File.ReadAllText("C:\\Users\\DESI\\Desktop\\Back-End-Test-Automation---Sept-2024\\2. Back-End Test Automation\\01. API Testing with C#\\01. Lab\\demoData.json");

            WeatherForecast forecastFromJson = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            //newtonsoft json package
            WeatherForecast forecastNS = new WeatherForecast();
                
            string WeatherForecastNS = JsonConvert.SerializeObject(forecastNS, Formatting.Indented);

            Console.WriteLine(WeatherForecastNS);

            jsonString = File.ReadAllText("C:\\Users\\DESI\\Desktop\\Back-End-Test-Automation---Sept-2024\\2. Back-End Test Automation\\01. API Testing with C#\\01. Lab\\demoData.json");

            WeatherForecast weatherInfoNS = JsonConvert.DeserializeObject<WeatherForecast>(jsonString);


            // working with anonymous objects
            var json = @"{ 'firstName': 'Svetlin',
            'lastName': 'Nakov',
            'jobTitle': 'Technical Trainer' }";

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JabTitle = string.Empty,
            };

            var person = JsonConvert.DeserializeAnonymousType(json, template);

            //applying naming convention to the class properties
            WeatherForecast weatherForecastResolver = new WeatherForecast();
            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            string snakeCaseJson = JsonConvert.SerializeObject(weatherForecastResolver, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            });

            Console.WriteLine(snakeCaseJson);

            //JObject
            var jsonAsString = JObject.Parse(@"{'products': [
            {'name': 'Fruits', 'products': ['apple', 'banana']},
            {'name': 'Vegetables', 'products': ['cucumber']}]}");

            var products = jsonAsString["products"].Select(t => string.Format("{0} ({1})", t["name"],string.Join(", ", t["products"])
            ));


        }
    }
}