using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class DestinationTests : IDisposable
    {
        private RestClient client;
        private string token;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(GlobalConstants.BaseUrl);
            token = GlobalConstants.AuthenticateUser("john.doe@example.com", "password123");

            Assert.That(token, Is.Not.Null.Or.Empty, "Authentication token should not be null or empty");
        }

        [Test]
        public void Test_GetAllDestinations()
        {
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the response content is a JSON array.
                // Assert that there is at least one destination in the array.

                // Assert that each destination's name is not null or empty.
                // Assert that each destination's location is not null or empty.
                // Assert that each destination's description is not null or empty.
                // Assert that each destination's category is not null or empty.
            });
        }

        [Test]
        public void Test_GetDestinationByName()
        {
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the location of "New York City" is correct.
                // Assert that the description of "New York City" matches the expected value.
            });
        }

        [Test]
        public void Test_AddDestination()
        {
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the destination name matches the input.
                // Assert that the destination location matches the input.
                // Assert that the category is not null or empty.
                // Assert that the description is not null or empty.
            });
        }

        [Test]
        public void Test_UpdateDestination()
        {
            // Assert that the GET request status code is 200 OK.
            // Assert that the GET request content is not empty.
            // Assert that the destination "Machu Picchu" is found.
            Assert.Multiple(() =>
            {
                // Assert that the update response status code is 200 OK.
                // Assert that the update response content is not empty.
                // Assert that the updated destination name matches the new value.
                // Assert that the updated best time to visit matches the new value.
                // Assert that the updated description is not null or empty.
            });
        }

        [Test]
        public void Test_DeleteDestination()
        {
            // Assert that the GET request status code is 200 OK.
            // Assert that the GET request content is not empty.
            // Assert that the destination "Yellowstone National Park" is found.
            Assert.Multiple(() =>
            {
                // Assert that the DELETE request status code is 200 OK.
                // Assert that the content of the deleted destination is null or "null".
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
