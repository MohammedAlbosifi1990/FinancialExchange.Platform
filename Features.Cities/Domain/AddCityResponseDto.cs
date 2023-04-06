using Shared.Core.Domain.Entities;

namespace Features.Cities.Domain;

public class AddCityResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }


    public static implicit operator AddCityResponseDto(City city)
    {
        return new AddCityResponseDto()
        {
            Name = city.Name,
            Id = city.Id
        };
    }

}