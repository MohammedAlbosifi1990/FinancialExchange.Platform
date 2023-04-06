using Features.Cities.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Shared.Core.Base;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Cities.Features.ReadCities;

public partial class CitiesController : PublicBaseController
{
    private readonly ICitiesRepository _citiesRepository;

    public CitiesController(ICitiesRepository citiesRepository)
    {
        _citiesRepository = citiesRepository;
    }
    
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<CityResponseDto>>> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetCityByIdCommand(id));

        return Ok(ApiResponse.Ok(result));
    }
    
    
    // [HttpGet]
    // public async Task<ActionResult<ApiResponse<IEnumerable<CityResponseDto>>>> GetAll(string? name)
    // {
    //     var result = await Mediator.Send(new GetAllCitiesCommand(name));
    //
    //     return Ok(ApiResponse.Ok(result));
    // }
    //
    
    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _citiesRepository.AsQueryable());
    }
}
