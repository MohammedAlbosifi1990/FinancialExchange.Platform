using Features.Authentications.Domain.Models.Authentications.Password;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Password.ChangePassword;

[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class PasswordController : PublicBaseController
{
    [HttpPost(RoutesConst.Authentications.ChangePassword)]
    public async Task<ActionResult<ApiResponse>> ChangePassword([FromBody] ChangePasswordRequestDto dto)
    {
        var result = await Mediator.Send(new ChangePasswordCommand(Guid.NewGuid(), dto.Password,dto.OldPassword));

        return result.IsSuccess
            ? Ok(ApiResponse.Ok(true))
            : BadRequest(ApiResponse.BadRequest(result.Message));
    }
}