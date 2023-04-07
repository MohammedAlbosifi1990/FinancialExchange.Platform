using Features.Cities.Domain;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.ReadCities;

public partial class CitiesController : PublicBaseController
{
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<CityResponseDto>>> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetCityByIdCommand(id));
        return Ok(ApiResponse.Ok((CityResponseDto)result.Data));
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CityResponseDto>>>> GetAll(string? name)
    {
        var result = await Mediator.Send(new GetAllCitiesCommand(name));

        var response = result.Data.Select(c => new CityResponseDto()
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
        return Ok(ApiResponse.Ok(response));
    }
    
    
    // [EnableQuery]
    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     return Ok(await _citiesRepository.AsQueryable());
    // }
}
