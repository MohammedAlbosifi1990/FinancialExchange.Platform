using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Password.ChangePassword;

public partial class PasswordController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.ChangePassword)]
    public async Task<ActionResult<ApiResponse>> ChangePassword([FromBody] string password,[FromBody] string oldPassword)
    {
        var result = await Mediator.Send(new ChangePasswordCommand(Guid.NewGuid(), password,oldPassword));

        return result.IsSuccess
            ? Ok(ApiResponse.Ok(true))
            : BadRequest(ApiResponse.BadRequest(result.Message));
    }
}