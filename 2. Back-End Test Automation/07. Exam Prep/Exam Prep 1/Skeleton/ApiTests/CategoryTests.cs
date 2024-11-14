using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class CategoryTests : IDisposable
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
        public void Test_CategoryLifecycle()
        {
            // Step 1: Create a new category
            // Assert that the response status code is 200 OK.
            // Assert that the category ID is not null or empty.

            // Step 2: Get all categories
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the response content is a JSON array.
                // Assert that there is at least one category in the array.
            });

            // Step 3: Get category by ID
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the category ID in the response matches the created category ID.
                // Assert that the category name matches the input name "Test Category".
            });

            // Step 4: Edit the category
            // Assert that the response status code is 200 OK.
            // Verify the category is updated
            Assert.Multiple(() =>
            {
                // Assert that the response status code is 200 OK.
                // Assert that the response content is not empty.
                // Assert that the updated category name matches the new value "Updated Test Category".
            });

            // Step 5: Delete the category
            // Assert that the response status code is 200 OK.
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
