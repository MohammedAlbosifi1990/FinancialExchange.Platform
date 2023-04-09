using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.RemoveOffice;

public class OfficesController : PublicBaseController
{
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ApiResponse>> EditOffice(Guid id)
    {
        var result = await Mediator.Send(new RemoveOfficeCommand(id));
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));

        return Ok(ApiResponse.Ok(true));
    }
}

