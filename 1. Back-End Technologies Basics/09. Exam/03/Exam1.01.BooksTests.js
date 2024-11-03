const { expect } = require("chai");
const { bookService } = require('./Exam1.01.Books.js');

describe("Book Service Tests", function() {

    describe("getBooks()", function() {
        it("Test: Should return a status 200 and an array of books", function () {
            const response = bookService.getBooks();
            expect(response.status).to.equal(200);
            expect(response.data).to.be.an("array").with.lengthOf(3);
            response.data.forEach((book) => {
                expect(book).to.have.all.keys(
                    "id",
                    "title",
                    "author",
                    "year",
                    "genre", 
                );
            });
        });
    });

    describe("addBook()", function() {

        it("Test: Should add a new book successfully", function () {
            const newBook = {
                id: "4",
                title: "Harry Potter and the Half-Blood Prince",
                author: "J.K.Rowling",
                year: 2015,
                genre: "Fantasy", 
            }
            const response = bookService.addBook(newBook);
            expect(response.status).to.equal(201);
            expect(response.message).to.equal("Book added successfully.");
            const books = bookService.getBooks().data;
            expect(books).to.deep.include(newBook);
        });

        it("Test: Should return status 400 when adding a book with missing fields", function () {
            const newBook = {
                id: "5",
                title: "Harry Potter",
                genre: "", 
            }

            const invalidBook = { title: "Harry Potter" };
            const response = bookService.addBook(invalidBook);
            expect(response.status).to.equal(400);
            expect(response.error).to.equal("Invalid Book Data!");
        });
    });

    describe("deleteBook(bookID)", function() {

        it("Should delete a book by id successfully", () => {
            const originalCount = bookService.books.length;
            const newBook = { id: "6", title: "Harry Potter and the Deathly Hallows", author: "J.K.Rowling",
            year: 2010,
            genre: "Fantasy", };
            const newResponse = bookService.addBook(newBook);
            expect(newResponse.status).to.equal(201);
            expect(bookService.books.length).to.equal(originalCount + 1);
            const deleteResponse = bookService.deleteBook("6");
            expect(deleteResponse.status).to.equal(200);
            expect(deleteResponse.message).to.equal("Book deleted successfully.");
            const deletedBook = bookService.books.find(book => book.id === "6");
            expect(deletedBook).to.be.undefined;
            expect(bookService.books.length).to.equal(originalCount);
        });      

        it("Should return status 404 when deleting a book with a non-existent id", () => {
            const response = bookService.deleteBook("not exist Id");
            expect(response.status).to.equal(404);
            expect(response.error).to.equal("Book Not Found!");
        });
    });

    describe("updateBook()", function() {

        it("Test: Should update a book successfully", function () {
            const oldBookID = "1";
            const updatedBook = {
              id: "1",
              title: "Harry Potter and the Philosopher's stone",
              author: "J.K.Rowling",
              year: 2000,
              genre: "Fantasy", 
            };
            const response = bookService.updateBook(oldBookID, updatedBook);
            expect(response.status).to.equal(200);
            expect(response.message).to.equal("Book updated successfully.");
            const updatedBookNew = bookService.books.find((book) => book.id === oldBookID);
            expect(updatedBookNew).to.deep.equal(updatedBook);
        });
      
        it("Test: Should return status 404 when updating a non-existent book", function () {
            const nonExistentBookName = "Non-Existent Book";
            const updatedBook = {
              id: "7",
              title: "Not Existing Book",
              author: "J.K.Rowling",
              year: 2099,
              genre: "Fantasy",
            };
            const response = bookService.updateBook(nonExistentBookName, updatedBook);
            expect(response.status).to.equal(404);
            expect(response.error).to.equal("Book Not Found!");
        });

        it("Test: Should return status 400 when updating with incomplete book data", function () {
            const oldBookID = "1";
            const incompletedBook = {
                id: "1",
                title: "Harry Potter and the Prisoner of Azkaban",
                genre: "Fantasy", 
            };
            const response = bookService.updateBook(oldBookID, incompletedBook);
            expect(response.status).to.equal(400);
            expect(response.error).to.equal("Invalid Book Data!");
        });
    });
});