using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace ApiTests
{
    [TestFixture]
    public class BookTests : IDisposable
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
        public void Test_GetAllBooks()
        {
            //Arrange
            var getRequest = new RestRequest("book", Method.Get);

            //Act
            var getResponse = client.Execute(getRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code is not OK");
                Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is null or empty");

                var books = JArray.Parse(getResponse.Content);

                Assert.That(books.Type, Is.EqualTo(JTokenType.Array), "The response content is not JSON Array");
                Assert.That(books.Count, Is.GreaterThan(0), "Expected books is less than zero");

                foreach (var book in books)
                {
                    Assert.That(book["title"]?.ToString(), Is.Not.Null.And.Not.Empty, "Property title is not as expected");
                    Assert.That(book["author"]?.ToString(), Is.Not.Null.And.Not.Empty, "Property author is not as expected");
                    Assert.That(book["description"]?.ToString(), Is.Not.Null.And.Not.Empty, "Property description is not as expected");
                    Assert.That(book
                        ["price"]?.ToString(), Is.Not.Null.And.Not.Empty, "Property price is not as expected");
                    Assert.That(book["pages"]?.ToString(), Is.Not.Null.And.Not.Empty, "Property pages is not as expected");
                    Assert.That(book["category"]?.Type, Is.EqualTo(JTokenType.Object), "Property category is not an object");
                }
            });
        }

        [Test]
        public void Test_GetBookByTitle()
        {
            //Arrange
            var getRequest = new RestRequest("book", Method.Get);

            //Act
            var getResponse = client.Execute(getRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code is not as expected");
                Assert.That(getResponse.Content, Is.Not.Null.And.Not.Empty, "Response content is not as expected");

                var books = JArray.Parse(getResponse.Content);
                var book = books.FirstOrDefault(b => b["title"]?.ToString() == "The Great Gatsby");

                Assert.That(book["author"]?.ToString(), Is.EqualTo("F. Scott Fitzgerald"), "The author property does not have the correct value");
            });
        }

        [Test]
        public void Test_AddBook()
        {
            //Arrange
            //Get all categories and extract first category id
            var getCategoriesRequest = new RestRequest("category", Method.Get);
            var getCategoriesResponse = client.Execute(getCategoriesRequest);
            Assert.That(getCategoriesResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response content is not as expected");
            Assert.That(getCategoriesResponse.Content, Is.Not.Null.And.Not.Empty, "Response content is not as expected");

            var categories = JArray.Parse(getCategoriesResponse.Content);
            var firstcategory = categories.First();
            var categoryId = firstcategory["_id"]?.ToString();

            //Create a new book
            var addRequest = new RestRequest("book", Method.Post);
            addRequest.AddHeader("Authorization", $"Bearer {token}");
            var title = "Harry Potter Title";
            var author = "J.K.Rowling";
            double price = 9.99;
            int pages = 100;
            addRequest.AddJsonBody(new
            {
                title,
                author,
                price,
                pages,
                category = categoryId
            });

            //Act
            var addResponse = client.Execute(addRequest);

            //Assert
            Assert.That(addResponse.StatusCode, Is.EqualTo(expected: HttpStatusCode.OK), "Response code is not as expected");
            Assert.That(addResponse.Content, Is.Not.Null.And.Not.Empty, "Response content is not as expected");
            
            var createdBook = JObject.Parse(addResponse.Content);
            Assert.That(createdBook["_id"]?.ToString(), Is.Not.Null.And.Not.Empty);
            var createdBookId = createdBook["_id"]?.ToString();

            //Get book by Id
            var getBookRequest = new RestRequest($"book/{createdBookId}", Method.Get);
            var getResponse = client.Execute(getBookRequest);

            Assert.Multiple(() =>
            {
                Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response code is not as expected");
                Assert.That(getResponse.Content, Is.Not.Null.And.Not.Empty, "Response content is not as expected");

                var book = JObject.Parse(getResponse.Content);

                Assert.That(book["title"]?.ToString(), Is.EqualTo(title));
                Assert.That(book["author"]?.ToString(), Is.EqualTo(author));
                Assert.That((double)book["price"], Is.EqualTo(price));
                Assert.That((int)book["pages"], Is.EqualTo(pages));
                Assert.That(book["category"].HasValues, Is. True);
                Assert.That(book["category"]["_id"]?.ToString(), Is.EqualTo(createdBookId));
            });
        }
               
        [Test]
        public void Test_UpdateBook()
        {
            //Arrange
            //Get all books and extract with title The Catcher in the Rye
            var getRequest = new RestRequest("book", Method.Get);

            var getResponse = client.Execute(getRequest);

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code is not OK");
            Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is null or empty");

            var books = JArray.Parse(getResponse.Content);
            var bookToUpdate = books.FirstOrDefault(b => b["title"]?.ToString() == "The Catcher in the Rye");

            Assert.That(bookToUpdate, Is.Not.Null);

            var bookId = bookToUpdate["_id"].ToString();

            //Create update request
            var updateRequest = new RestRequest("book/{id}", Method.Put);
            updateRequest.AddHeader("Authorization", $"Bearer {token}");
            updateRequest.AddUrlSegment("id", bookId);
            updateRequest.AddJsonBody(new
            {
                title = "Updated Book Title",
                author = "Updated Author",
            });

            //Act
            var updateResponse = client.Execute(updateRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code OK (200)");
                Assert.That(updateResponse.Content, Is.Not.Empty, "Update response content should not be empty");

                var content = JObject.Parse(updateResponse.Content);

                Assert.That(content["title"]?.ToString(), Is.EqualTo("Updated Book Title"), "Book title should match the updated value");
                Assert.That(content["author"]?.ToString(), Is.EqualTo("Updated Author"), "The book author should match the updated value");
            });
        }

        [Test]
        public void Test_DeleteBook()
        {
            //Arrange
            //Get all books and extract with title To Kill a Mockingbird
            var getRequest = new RestRequest("book", Method.Get);

            var getResponse = client.Execute(getRequest);

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code is not OK");
            Assert.That(getResponse.Content, Is.Not.Null.Or.Empty, "Response content is null or empty");

            var books = JArray.Parse(getResponse.Content);
            var bookToDelete = books.FirstOrDefault(b => b["title"]?.ToString() == "To Kill a Mockingbird");

            Assert.That(bookToDelete, Is.Not.Null);

            var bookId = bookToDelete["_id"]?.ToString();

            //Create delete request
            var deleteRequest = new RestRequest($"book/{bookId}", Method.Delete);
            deleteRequest.AddHeader("Authorization", $"Bearer {token}");

            //Act
            var deleteResponse = client.Execute(deleteRequest);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                //Get request to get the book that we deleted
                var verifyRequest = new RestRequest($"book/{bookId}");
                var verifyResponse = client.Execute(verifyRequest);

                Assert.That(verifyResponse.Content, Is.EqualTo("null"));
            });
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
