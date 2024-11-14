using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class RecipeTests : IDisposable
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
        public void Test_GetAllRecipes()
        {
            Assert.Multiple(() =>
            {
                // Verify the status code is 200 OK
                // Ensure response content is not empty
                // Check if the response is a JSON array
                // Ensure there is at least one recipe in the response
                    // Validate that essential recipe fields are not null or empty
            });
        }

        [Test]
        public void Test_GetRecipeByTitle()
        {
            Assert.Multiple(() =>
            {
                // Verify the status code is 200 OK
                // Ensure response content is not empty
                // Verify the recipe with the title 'Chocolate Chip Cookies' exists
                // Ensure the essential fields are not null or empty
            });
        }

        [Test]
        public void Test_AddRecipe()
        {
            Assert.Multiple(() =>
            {
                // Verify the status code is 200 OK
                // Ensure response content is not empty
                // Validate the added recipe fields
            });
        }

        [Test]
        public void Test_UpdateRecipe()
        {
            // Step 1: Retrieve all recipes
            // Verify the recipes are retrieved successfully
            // Ensure the recipe to update exists

            // Step 2: Update the specific recipe
            Assert.Multiple(() =>
            {
                // Verify the update was successful
                // Ensure response content is not empty
                // Validate the updated recipe fields
            });
        }

        [Test]
        public void Test_DeleteRecipe()
        {
            // Step 1: Retrieve all recipes
            // Ensure the recipes are retrieved successfully
            // Ensure the recipe to delete exists

            // Step 2: Delete the specific recipe
            Assert.Multiple(() =>
            {
                // Verify the recipe deletion was successful

                // Step 3: Verify that the recipe is deleted by attempting to retrieve it again
                // Ensure the recipe is not found after deletion
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
