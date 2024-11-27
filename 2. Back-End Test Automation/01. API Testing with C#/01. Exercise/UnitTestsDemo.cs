using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Dynamic;
using System.Net;
using System.Text.Json;

namespace DemoNunitTest
{
    public class UnitTestsDemo
    {
        private RestClient client;

        string issuesEndPoint = "/repos/testnakov/test-nakov-repo/issues";

        Random random;

        int createdIssueNumber;

        [SetUp]
        public void Setup()
        {
            var options = new RestClientOptions("https://api.github.com")
            {
                MaxTimeout = 3000,
                Authenticator = new HttpBasicAuthenticator("desito07", "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")
            };

            client = new RestClient(options);

            random = new Random();

         }

        [Test]
        public void Test_GitHubApiRequest()
        {

            var request = new RestRequest(issuesEndPoint, Method.Get);

            var response = client.Get(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }

        [Test]
        public void Test_CreateNewIssue()
        {
            var request = new RestRequest(issuesEndPoint);
            request.AddBody(new { title = "SomeRandonTitle456", body = "SomeRandomBody456" });

            var response = client.Post(request);

            var issue = System.Text.Json.JsonSerializer.Deserialize<Issue>(response.Content);

            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.EqualTo("SomeRandonTitle456"));
            Assert.That(issue.body, Is.EqualTo("SomeRandomBody456"));
        }

        [Test]
        public void Test_GetAllIssuesFromRepo()
        {
            //Arrange
            var request = new RestRequest(issuesEndPoint, Method.Get);

            //Act
            var response = client.Execute(request);
            var issues = System.Text.Json.JsonSerializer.Deserialize<List<Issue>>(response.Content);

            //Assert
            Assert.That(issues.Count > 1);

            foreach(var issue in issues)
            {
                Assert.That(issue.id, Is.GreaterThan(0));
                Assert.That(issue.number, Is.GreaterThan(0));
                Assert.That(issue.title, Is.Not.Empty);
            }
        }

        private Issue CreateIssue(string title, string body)
        {
            var request = new RestRequest(issuesEndPoint);
            request.AddBody(new {body, title});

            var response = client.Execute(request, Method.Post);
            var issue = JsonConvert.DeserializeObject<Issue>(response.Content);

            return issue;
        }

        public string GenerateRandomStringByPrefix(string prefix)
        {
            int randomNumber = random.Next(9999, 1000000);
            return prefix + randomNumber;
        }

        [Test]
        public void Test_CreateGithubIssue()
        {

            //Arrange
            string title = GenerateRandomStringByPrefix("Title");

            string body = GenerateRandomStringByPrefix("body");


            //Act
            var issue = CreateIssue(title, body);
            createdIssueNumber = issue.number;

            //Assert

            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.EqualTo(title));
            Assert.That(issue.body, Is.EqualTo(body));
           
        }

        [Test]
        public void Test_UpdateGitHubIssue()
        {
            //Arrange
            string updatedTitle = "Updated Title Desi";
            var request = new RestRequest(issuesEndPoint + "/" + createdIssueNumber.ToString());

            request.AddBody(new
            {
                title = updatedTitle
            });

            //Act
            var response = client.Execute<Issue>(request, Method.Patch);
            var issue = JsonConvert.DeserializeObject<Issue>(response.Content);

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(issue.id, Is.GreaterThan(0));
            Assert.That(issue.number, Is.GreaterThan(0));
            Assert.That(issue.title, Is.EqualTo(updatedTitle));
        }
    }
}