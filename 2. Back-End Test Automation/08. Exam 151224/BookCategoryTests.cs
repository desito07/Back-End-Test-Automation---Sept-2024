using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class BookCategoryTests : IDisposable
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
        public void Test_BookCategoryLifecycle()
        {
            // Step 1: Create a new book category

            var createBookCategoryRequest = new RestRequest("category", Method.Post);
            createBookCategoryRequest.AddHeader("Authorization", $"Bearer {token}");
            createBookCategoryRequest.AddJsonBody(new
            {
                title = "Fictional Literature"
            });

            var createBookCategoryResponse = client.Execute(createBookCategoryRequest);
            Assert.That(createBookCategoryResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");

            var createdBookCategory = JObject.Parse(createBookCategoryResponse.Content);
            var categoryBookId = createdBookCategory["_id"]?.ToString();
            Assert.That(categoryBookId, Is.Not.Null.And.Not.Empty, "Category Book ID should not be null or empty");
            Assert.That(createdBookCategory["title"]?.ToString(), Is.EqualTo("Fictional Literature"), "The title property does not have the correct value");


            // Step 2: Retrieve all book categories and verify the newly created category is present

            var getAllBookRequest = new RestRequest("category", Method.Get);
            var getAllBookResponse = client.Execute(getAllBookRequest);

            Assert.Multiple(() =>
            {
            Assert.That(getAllBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");
            Assert.That(getAllBookResponse.Content, Is.Not.Empty, "Response content should not be empty");

            var bookCategories = JArray.Parse(getAllBookResponse.Content);
            Assert.That(bookCategories.Type, Is.EqualTo(JTokenType.Array), "Expected response content to be a JSON array");
            Assert.That(bookCategories.Count, Is.GreaterThan(0), "Expected at least one category in the response");
            Assert.That(createdBookCategory, Is.Not.Null);

            });

            var getByIdRequest = new RestRequest($"category/{categoryBookId}", Method.Get);
            var getByIdResponse = client.Execute(getByIdRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getByIdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");
                Assert.That(getByIdResponse.Content, Is.Not.Empty, "Response content should not be empty");

                var bookCategory = JObject.Parse(getByIdResponse.Content);
                Assert.That(bookCategory["_id"]?.ToString(), Is.EqualTo(categoryBookId), "Expected the category ID to match");
            });

                // Step 3: Update the category title

            var editBookRequest = new RestRequest($"category/{categoryBookId}", Method.Put);
            editBookRequest.AddHeader("Authorization", $"Bearer {token}");
            editBookRequest.AddJsonBody(new
            {
                title = "Updated Fictional Literature"
            });

            var editResponse = client.Execute(editBookRequest);
            Assert.That(editResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");


            // Step 4: Verify that the category details have been updated

            var getUpdatedBookCategoryRequest = new RestRequest($"category/{categoryBookId}", Method.Get);
            var getUpdatedBookCategoryResponse = client.Execute(getUpdatedBookCategoryRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getUpdatedBookCategoryResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");
                Assert.That(getUpdatedBookCategoryResponse.Content, Is.Not.Empty, "Response content should not be empty");

                var updatedBookCategory = JObject.Parse(getUpdatedBookCategoryResponse.Content);
                Assert.That(updatedBookCategory["title"]?.ToString(), Is.EqualTo("Updated Fictional Literature"), "Expected the updated category name to match");
            });

            // Step 5: Delete the category and validate it's no longer accessible

            var deleteBookRequest = new RestRequest($"category/{categoryBookId}", Method.Delete);
            deleteBookRequest.AddHeader("Authorization", $"Bearer {token}");

            var deleteBookResponse = client.Execute(deleteBookRequest);
            Assert.That(deleteBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");

            // Step 6: Verify that the deleted category cannot be found

            var getDeletedBookCategoryRequest = new RestRequest($"category/{categoryBookId}", Method.Get);
            var getDeletedBookCategoryResponse = client.Execute(getDeletedBookCategoryRequest);

            Assert.That(getDeletedBookCategoryResponse.Content, Is.Empty.Or.EqualTo("null"), "Deleted category should not be found");
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
