using Shared.Core.Domain.Entities;

namespace Features.Cities.Domain;

public class EditCityResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }


    public static implicit operator EditCityResponseDto(City city)
    {
        return new EditCityResponseDto()
        {
            Name = city.Name,
            Id = city.Id
        };
    }

}