using System.ComponentModel.DataAnnotations;

namespace Store.API.Dtos;

public record class CreateGameDto(
    [Required][StringLength(50)] string Name,
    //[Required][StringLength(20)] string Genre,
    int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
