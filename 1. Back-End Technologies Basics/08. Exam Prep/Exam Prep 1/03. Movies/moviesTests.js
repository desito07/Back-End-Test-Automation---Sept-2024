const { expect } = require("chai");
const { movieService } = require("./Prep1.03.Movies.js"); // провери къдравите скоби и правилния път на файла, 
// също така и в package.json

describe("movieService Tests", function () {

  describe("getMovies()", function () {
    it("Test: Should return all movies with status 200", function () {
      const response = movieService.getMovies();
      expect(response.status).to.equal(200);
      expect(response.data).to.be.an("array").with.lengthOf(3);
      response.data.forEach((movie) => {
        expect(movie).to.have.all.keys(
          "id",
          "name",
          "genre",
          "year",
          "director",
          "rating",
          "duration",
          "language",
          "desc"
        );
      });
    });
  });

  describe("addMovie(movie)", function () {
    it("Test: Should successfully add a new movie", function () {
      const newMovie = {
        id: "4",
        name: "Avatar",
        genre: "Sci-Fi",
        year: 2009,
        director: "James Cameron",
        rating: 7.8,
        duration: 162,
        language: "English",
        desc:
          "A paraplegic Marine is dispatched to the moon Pandora on a unique mission.",
      };
      const response = movieService.addMovie(newMovie);
      expect(response.status).to.equal(201);
      expect(response.message).to.equal("Movie added successfully.");
      expect(movieService.movies).to.include.deep.members([newMovie]);
    });

    it("Test: Should return an error for invalid movie data", function () {
      const invalidMovie = {
        id: "5",
        name: "Avatar",
        genre: "Sci-Fi",
        year: 2009,
        director: "James Cameron",
        rating: 7.8,
        duration: 162,
        language: "English",
        // Missing 'desc'
      };
      const response = movieService.addMovie(invalidMovie);
      expect(response.status).to.equal(400);
      expect(response.error).to.equal("Invalid Movie Data!");
    });
  });

  describe("deleteMovie(movieId)", function () {
    it("Test: Should delete a movie by id successfully", function () {
      const movieId = "4"; // assuming movie with id "4" was added in a previous test
      const response = movieService.deleteMovie(movieId);
      expect(response.status).to.equal(200);
      expect(response.message).to.equal("Movie deleted successfully.");
      expect(movieService.movies.some((movie) => movie.id === movieId)).to.be
        .false;
    });

    it("Test: Should return 404 for a non-existent movie id", function () {
      const nonExistentMovieId = "999";
      const response = movieService.deleteMovie(nonExistentMovieId);
      expect(response.status).to.equal(404);
      expect(response.error).to.equal("Movie Not Found!");
    });
  });

  describe("updateMovie(oldName, newMovie)", function () {
    it("Test: Should update an existing movie successfully", function () {
      const oldName = "Inception";
      const updatedMovie = {
        id: "1",
        name: "Inception",
        genre: "Sci-Fi",
        year: 2010,
        director: "Christopher Nolan",
        rating: 9.0,
        duration: 148,
        language: "English",
        desc:
          "A skilled thief is given a chance at redemption if he can successfully plant an idea in someone's mind.",
      };
      const response = movieService.updateMovie(oldName, updatedMovie);
      expect(response.status).to.equal(200);
      expect(response.message).to.equal("Movie updated successfully.");
      const movie = movieService.movies.find((movie) => movie.name === oldName);
      expect(movie).to.deep.equal(updatedMovie);
    });

    it("Test: Should return an error if the movie to update does not exist", function () {
      const nonExistentName = "Non-Existent Movie";
      const updatedMovie = {
        id: "10",
        name: "Non-Existent Movie",
        genre: "Action",
        year: 2020,
        director: "Unknown",
        rating: 5.5,
        duration: 120,
        language: "English",
        desc: "A non-existent movie plot.",
      };
      const response = movieService.updateMovie(nonExistentName, updatedMovie);
      expect(response.status).to.equal(404);
      expect(response.error).to.equal("Movie Not Found!");
    });

    it("Test: Should return an error if the new movie data is invalid", function () {
      const oldName = "Interstellar";
      const invalidMovieData = {
        id: "3",
        name: "Interstellar",
        genre: "Adventure",
        year: 2014,
        director: "Christopher Nolan",
        // Missing fields: rating, duration, language, desc
      };
      const response = movieService.updateMovie(oldName, invalidMovieData);
      expect(response.status).to.equal(400);
      expect(response.error).to.equal("Invalid Movie Data!");
    });
  });
});
