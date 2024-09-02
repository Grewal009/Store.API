using Store.API.Data;
using Store.API.Dtos;
using Store.API.Entities;

namespace Store.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new(1, "Street Fighter","Fighting",19.99M,new DateOnly(2020,10,15)),
        new(2, "Final Fantasy","Fantasy",29.99M,new DateOnly(2022,1,22)),
        new(3, "FIFA23","Sports",39.99M,new DateOnly(2024,6,25)),
        ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(x => x.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);
        //give name to the endpoint to access the created resource .WithName("GetGame")


        group.MapPost("/", (CreateGameDto newGame, StoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate

            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDto gameDto = new(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
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

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });


        return group;
    }

}
