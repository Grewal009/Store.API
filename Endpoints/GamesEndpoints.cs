using System;
using Store.API.Dtos;

namespace Store.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new(1, "Street Fighter","Fighting",19.99M,new DateOnly(2020,10,15)),
        new(2, "Final Fantasy","Fantasy",29.99M,new DateOnly(2022,1,22)),
        new(3, "FIFA23","Sports",39.99M,new DateOnly(2024,6,25)),
        ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {

        app.MapGet("games", () => games);

        app.MapGet("games/{id}", (int id) =>
        {
            GameDto? game = games.Find(x => x.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);
        //give name to the endpoint to access the created resource .WithName("GetGame")


        app.MapPost("games", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var gameIndex = games.FindIndex(game => game.Id == id);

            if (gameIndex == -1)
            {
                return Results.NotFound();
            }

            games[gameIndex] = new(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
                    );
            return Results.NoContent();
        });

        app.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });


        return app;
    }

}
