using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Logout;

[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class LogoutController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.Logout)]
    public async Task<ActionResult<ApiResponse>> Logout()
    {
        var result = await Mediator.Send(new LogoutCommand(User.UserId()));
        if (result.IsSuccess)
            return Ok(ApiResponse.Ok(true));
        
        return BadRequest(ApiResponse.BadRequest(result.Message));
    }
}