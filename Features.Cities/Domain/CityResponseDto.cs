using Shared.Core.Domain.Entities;

namespace Features.Cities.Domain;

public class CityResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }


    public static implicit operator CityResponseDto(City city)
    {
        return new CityResponseDto()
        {
            Name = city.Name,
            Id = city.Id
        };
    }

}