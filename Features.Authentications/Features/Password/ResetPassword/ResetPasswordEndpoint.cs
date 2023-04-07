using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Password.ResetPassword;

public partial class PasswordController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.ResetPassword)]
    public async Task<ActionResult<ApiResponse>> ChangePassword(
        [FromBody] string code,[FromBody] string password)
    {
        var result = await Mediator.Send(new ResetPasswordCommand(Guid.NewGuid(), code ,password));

        return result.IsSuccess
            ? Ok(ApiResponse.Ok(true))
            : BadRequest(ApiResponse.BadRequest(result.Message));
    }
}