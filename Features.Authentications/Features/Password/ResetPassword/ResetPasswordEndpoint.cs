using Features.Authentications.Domain.Models.Authentications.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Password.ResetPassword;
//TODO [Authorize]
[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class PasswordController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.ResetPassword)]
    public async Task<ActionResult<ApiResponse>> ChangePassword(
        [FromBody] ResetPasswordRequestDto dto)
    {
        var result = await Mediator.Send(new ResetPasswordCommand(Guid.NewGuid(), dto.Code ,dto.Password));

        return result.IsSuccess
            ? Ok(ApiResponse.Ok(true))
            : BadRequest(ApiResponse.BadRequest(result.Message));
    }
}