using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DemoNunitTest
{
    public class UnitTestsDemo
    {
        RestClient client;

        Random random;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            {
                MaxTimeout = 3000,
                Authenticator = new HttpBasicAuthenticator("desito07", "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")
            };
            this.client = new RestClient("https://api.github.com");

            random = new Random();
        }

        [TearDown]
        public void Teardown()
        {
            this.client.Dispose();
        }


        [Test]
        public void Test_GithubApiRequest()
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);

            //request.Timeout = TimeSpan.FromMilliseconds(1000);

            var response = client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void Test_GetAllIssuesFromRepo()
        {
            //Arrange
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Get);

            //Act
            var response = this.client.Execute(request);
            var issues = System.Text.Json.JsonSerializer.Deserialize<List<Issue>>(response.Content);

            //Assert
            Assert.That(issues.Count > 1);

            foreach (var issue in issues)
            {
                Assert.That(issue.id, Is.GreaterThan(0));
                Assert.That(issue.number, Is.GreaterThan(0));
                Assert.That(issue.title, Is.Not.Empty);
            }

        }

        private Issue CreateIssue(string title, string body)
        {
            var request = new RestRequest("/repos/testnakov/test-nakov-repo/issues");
            request.AddBody(new { body, title });

            var response = client.Execute(request, Method.Post);
            var issue = System.Text.Json.JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;

        }
        [Test]
        public void Test_CreateGithubIssue()
        {
            //Arrange
            string title = "Title";
            string body = "Body";

            //Act
            var issue = CreateIssue(title, body);

            //Assert
            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.EqualTo(title));
            Assert.That(issue.body, Is.EqualTo(body));
        }

        [Test]
        public void Test_EditIssue()
        {
            var request = new RestRequest("repos/testnakov/test-nakov-repo/issues/4946");
            request.AddJsonBody(new
            {
                title = "Changing the name"
            }
            );

            var response = client.Execute(request, Method.Patch);
            var issue = System.Text.Json.JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(issue.id, Is.GreaterThan(0), "Issue ID should be greater than 0.");
            Assert.That(response.Content, Is.Not.Empty, "The response content should not be empty.");
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number should be greater than 0.");
            Assert.That(issue.title, Is.EqualTo("Changing the name of the issue that I created"), "The issue title should match the new title.");
        }

     }
}