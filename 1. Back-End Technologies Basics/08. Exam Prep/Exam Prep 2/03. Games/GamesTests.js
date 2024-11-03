const { expect } = require("chai");

const { gameService } = require('./Prep2.03.Games.js');

describe("gameService", () => {
    // Sample data for testing
    const sampleGame = {
        id: "4",
        title: "Cyberpunk 2077",
        genre: "RPG",
        year: 2020,
        developer: "CD Projekt Red",
        description: "A futuristic RPG set in Night City."
    };

    describe("getGames()", () => {
        it("should return a status of 200 and an array of games", () => {
            const response = gameService.getGames();
            expect(response.status).to.equal(200);
            expect(response.data).to.be.an("array").with.lengthOf(3);
            expect(response.data[0]).to.include.keys("id", "title", "genre", "year", "developer", "description");
        });
    });

    describe("addGame(game)", () => {
        it("should add a new game and return a status of 201 with a success message", () => {
            const response = gameService.addGame(sampleGame);
            expect(response.status).to.equal(201);
            expect(response.message).to.equal("Game added successfully.");
            const games = gameService.getGames().data;
            expect(games).to.deep.include(sampleGame);
        });

        it("should return a status of 400 with an error message if data is missing", () => {
            const invalidGame = { title: "Incomplete Game" };
            const response = gameService.addGame(invalidGame);
            expect(response.status).to.equal(400);
            expect(response.error).to.equal("Invalid Game Data!");
        });
    });

    describe("deleteGame(gameId)", () => {
        it("should delete a game by id and return a status of 200 with a success message", () => {
            const response = gameService.deleteGame("4");
            expect(response.status).to.equal(200);
            expect(response.message).to.equal("Game deleted successfully.");
            const games = gameService.getGames().data;
            expect(games.find(game => game.id === "4")).to.be.undefined;
        });

        it("should return a status of 404 with an error message if the gameId is not found", () => {
            const response = gameService.deleteGame("nonexistentId");
            expect(response.status).to.equal(404);
            expect(response.error).to.equal("Game Not Found!");
        });
    });

    describe("updateGame(oldId, newGame)", () => {
        it("should update a game and return a status of 200 with a success message", () => {
            const updatedGame = {
                id: "2",
                title: "God of War (Updated)",
                genre: "Action-adventure",
                year: 2018,
                developer: "Santa Monica Studio",
                description: "Updated description."
            };
            const response = gameService.updateGame("2", updatedGame);
            expect(response.status).to.equal(200);
            expect(response.message).to.equal("Game updated successfully.");
            const games = gameService.getGames().data;
            expect(games.find(game => game.id === "2")).to.deep.equal(updatedGame);
        });

        it("should return a status of 404 with an error message if the oldId is not found", () => {
            const response = gameService.updateGame("nonexistentId", sampleGame);
            expect(response.status).to.equal(404);
            expect(response.error).to.equal("Game Not Found!");
        });

        it("should return a status of 400 with an error message if the new game data is invalid", () => {
            const invalidGame = { title: "Invalid Game" }; // Missing required fields
            const response = gameService.updateGame("1", invalidGame);
            expect(response.status).to.equal(400);
            expect(response.error).to.equal("Invalid Game Data!");
        });
    });
});
