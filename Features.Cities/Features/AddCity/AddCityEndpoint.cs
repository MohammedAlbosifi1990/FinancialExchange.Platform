using Features.Cities.Domain;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.AddCity;

public partial class CitiesController : PublicBaseController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<AddCityResponseDto>>> AddCity([FromBody] AddCityCommand requestDto)
    {
        var result = await Mediator.Send(requestDto);
        return Ok(ApiResponse.Ok(result));
    }
}
