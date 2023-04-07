using Features.Authentications.Domain.Models.Authentications.Confirmations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.CodeConfirmation;

[Authorize]
public partial class ConfirmationController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.Confirm)]
    public async Task<ActionResult<ApiResponse>> SignInByEmail([FromBody] CodeConfirmationCommand requestDto)
    {
        IApiResult result = await Mediator.Send(requestDto);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));
        return Ok(ApiResponse.Ok(result.Data));
    }
}