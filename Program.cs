using Store.API.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
    new(1, "Street Fighter","Fighting",19.99M,new DateOnly(2020,10,15)),
    new(2, "Final Fantasy","Fantasy",29.99M,new DateOnly(2022,1,22)),
    new(3, "FIFA23","Sports",39.99M,new DateOnly(2024,6,25)),
];

app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) => games.Find(x => x.Id == id));

app.Run();
