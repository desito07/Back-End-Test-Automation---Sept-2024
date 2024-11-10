using RestSharp;
using System.Net;

namespace DemoNunitTest
{
    public class UnitTestsDemo
    {
        private RestClient client;


        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            {
                MaxTimeout = 3000
            };
            this.client = new RestClient(options);
        }

        [Test]
        public void Test_GithubApiRequest()
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);

            request.Timeout = TimeSpan.FromMilliseconds(1);

            var response = client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Status code is not as expected");
        }
    }
}