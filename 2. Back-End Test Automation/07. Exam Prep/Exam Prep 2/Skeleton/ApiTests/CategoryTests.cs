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
        public void Test_CategoryLifecycle_RecipeBook()
        {
            // Step 1: Create a new category
            // Verify status code is 200 OK
            // Ensure category ID is not null or empty
            // Ensure category name matches the input

            // Step 2: Get all categories and verify new category is included
            
            Assert.Multiple(() =>
            {
                // Verify status code is 200 OK
                // Ensure response content is not empty
                // Ensure the response is a JSON array
                // Ensure at least one category is returned
                // Ensure the created category is present in the list
            });

            // Step 3: Get category by ID
            Assert.Multiple(() =>
            {
                // Verify status code is 200 OK
                // Ensure response content is not empty
                // Ensure category ID matches
                // Ensure category name matches
            });

            // Step 4: Edit the category and verify update
            // Verify status code is 200 OK
            // Verify the category is updated
            Assert.Multiple(() =>
            {
                // Verify status code is 200 OK
                // Ensure response content is not empty
                // Ensure updated category name matches
            });

            // Step 5: Attempt to update category with invalid data (negative test)
            // Verify category name is not updated due to invalid input

            // Step 6: Delete the category
            // Verify status code is 200 OK

            // Step 7: Verify category is deleted
            // Ensure the category is not found after deletion
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
