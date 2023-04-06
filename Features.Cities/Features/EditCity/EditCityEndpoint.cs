using Features.Cities.Domain;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.EditCity;

public partial class CitiesController : PublicBaseController
{
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<EditCityResponseDto>>> EditCity(Guid id,[FromBody] string name)
    {
        var result = await Mediator.Send(new EditCityCommand(id,name));
        return Ok(ApiResponse.Ok(result));
    }
}
