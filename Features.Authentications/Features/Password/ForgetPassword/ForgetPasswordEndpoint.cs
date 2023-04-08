using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Password.ForgetPassword;

[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class PasswordController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.ForGetPassword)]
    public async Task<ActionResult<ApiResponse>> ForGetPassword()
    {
        var result = await Mediator.Send(new ForgetPasswordCommand(Guid.NewGuid()));

        return result.IsSuccess
            ? Ok(ApiResponse.Ok(true))
            : BadRequest(ApiResponse.BadRequest(result.Message));
    }
}