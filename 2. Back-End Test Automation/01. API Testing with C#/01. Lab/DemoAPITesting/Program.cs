using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;

namespace DemoAPITesting

{
    class Program
    {
        public static object responsePost;

        static void Main(string[] args)
        {
            //built-in system.text.json nugget package
            WeatherForecast forecast = new WeatherForecast();
            string weatherInfo = System.Text.Json.JsonSerializer.Serialize(forecast);
            Console.WriteLine(weatherInfo);

            string jsonString = File.ReadAllText("C:\\Users\\Raya\\OneDrive\\Desktop\\Desi's stuff\\QA\\API Testing with C#\\demo.json");

            WeatherForecast forecastFromJson = System.Text.Json.JsonSerializer.Deserialize<WeatherForecast>(jsonString);

            //newtonsoft json package
            WeatherForecast forecastNS = new WeatherForecast();
            string weatherForecastNS = JsonConvert.SerializeObject(forecastNS, Formatting.Indented);
            Console.WriteLine(weatherForecastNS);

            jsonString = File.ReadAllText("C:\\Users\\Raya\\OneDrive\\Desktop\\Desi's stuff\\QA\\API Testing with C#\\demo.json");
            WeatherForecast weatherInfoNS = JsonConvert.DeserializeObject<WeatherForecast>(jsonString);

            //working with annoymous objects
            var json = @"{  'firstName': 'Svetlin',
                            'lastName': 'Nakov',
                            'jobTitle': 'Technical Trainer' }";

            var template = new
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                JobTitle = string.Empty
            };
                                               
            var person = JsonConvert.DeserializeAnonymousType(json, template);

            //applying naming convention to the class properties
           
            WeatherForecast weatherForecastResolver = new WeatherForecast();
            
            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            string snakeCaseJson = JsonConvert.SerializeObject(weatherForecastResolver, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            });

            Console.WriteLine(snakeCaseJson);

            //Jobject

            var jsonAsString = JObject.Parse(@"{'products': [
             {'name': 'Fruits', 'products': ['apple', 'banana']},
            {'name': 'Vegetables', 'products': ['cucumber']}]}");

            var products = jsonAsString["products"].Select(t =>
            string.Format("{0} ({1})",
            t["name"],
            string.Join(", ", t["products"])
            ));

            //Executingsimple HTTP get request
            var client = new RestClient("https://api.github.com");

            var request = new RestRequest("/users/softuni/repos", Method.Get);

            var response = client.Execute(request);


            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Content);

            //using URL Segments
            var requestURLSegments = new RestRequest("/repos/{user}/{repo}/issues/{id}", Method.Get);

            requestURLSegments.AddUrlSegment("user", "testnakov");
            requestURLSegments.AddUrlSegment("repo", "test-nakov-repo");
            requestURLSegments.AddUrlSegment("id", 1);

            var responseURLSegment = client.Execute(requestURLSegments);
            Console.WriteLine(responseURLSegment.StatusCode);
            Console.WriteLine(responseURLSegment.Content);


            //deserializing json response

            var requestDeserializing = new RestRequest("/users/softuni/repos", Method.Get);

            var responseDeserializing = client.Execute(requestDeserializing);

            var repos = JsonConvert.DeserializeObject<List<Repo>>(responseDeserializing.Content);

            //https post with authentication

            var clientWithAuthentication = new RestClient(new RestClientOptions("https://api.github.com")
            {
                Authenticator = new HttpBasicAuthenticator("desito07", "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")

            });

            var postRequest = new RestRequest("(/repos/testnakov/test-nakov-repo/issues", Method.Post);

            postRequest.AddHeader("Content-type", "application/json");
            postRequest.AddJsonBody(new { title = "SomeTitle", body = "SomeBody" });

            var responsePost = clientWithAuthentication.Execute(postRequest);

            Console.WriteLine(responsePost.StatusCode);
            Console.WriteLine(responsePost.Content);    
            

              

        }
    }
}
