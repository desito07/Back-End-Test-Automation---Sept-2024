using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http.Headers;
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

            //Console.WriteLine(weatherInfo);

            string jsonString = File.ReadAllText("C:\\Users\\DESI\\Desktop\\Back-End-Test-Automation---Sept-2024\\2. Back-End Test Automation\\01. API Testing with C#\\01. Lab\\demoData.json");

            WeatherForecast forecastFromJson = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            //newtonsoft json package
            WeatherForecast forecastNS = new WeatherForecast();
                
            string WeatherForecastNS = JsonConvert.SerializeObject(forecastNS, Formatting.Indented);

            //Console.WriteLine(WeatherForecastNS);

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

            //Console.WriteLine(snakeCaseJson);

            //JObject
            var jsonAsString = JObject.Parse(@"{'products': [
            {'name': 'Fruits', 'products': ['apple', 'banana']},
            {'name': 'Vegetables', 'products': ['cucumber']}]}");

            var products = jsonAsString["products"].Select(t => string.Format("{0} ({1})", t["name"],string.Join(", ", t["products"])
            ));

            //Executing simple HTTP GET request
            var client = new RestClient("https://api.github.com");

            var request = new RestRequest("/users/softuni/repos", Method.Get);

            var response = client.Execute(request);

            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Content);

            //using URL segment
            var requestURLSegments = new RestRequest("/repos/{user}/{repo}/issues/{id}", Method.Get);

            requestURLSegments.AddUrlSegment("user", "testnakov");
            requestURLSegments.AddUrlSegment("repo", "test-nakov-repo");
            requestURLSegments.AddUrlSegment("id", 1);

            var responseURLSEgment = client.Execute(requestURLSegments);
            //Console.WriteLine(responseURLSEgment.StatusCode);
            //Console.WriteLine(responseURLSEgment.Content);

            //deserialing json response
            var requestDesetializing = new RestRequest("/users/softuni/repos", Method.Get);
            
            var respondDeserializing = client.Execute(requestDesetializing);

            var repos = JsonConvert.DeserializeObject<List<Repo>>(respondDeserializing.Content);

            //http post with authenitication
            var clientWithAuthentication = new RestClient(new RestClientOptions("https://api.github.com")
            {
                Authenticator = new HttpBasicAuthenticator("desito07", "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")
            });

            var postRequest = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Post);
            postRequest.AddHeader("Content-type", "application/json");
            postRequest.AddJsonBody(new { title = "SomeTitle", body = "SomeBody" });

            var responsePost = clientWithAuthentication.Execute(postRequest);

            Console.WriteLine(responsePost.StatusCode); //Forbidden status 403
            Console.WriteLine(responsePost.Content); //Forbidden status 403
        }
    }
}
