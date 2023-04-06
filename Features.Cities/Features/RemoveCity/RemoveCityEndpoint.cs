using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.RemoveCity;

public partial class CitiesController : PublicBaseController
{
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveCity(Guid id)
    {
        var result = await Mediator.Send(new RemoveCityCommand(id));

        return Ok(ApiResponse.Ok(result));
    }
}
