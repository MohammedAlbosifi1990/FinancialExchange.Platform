// ReSharper disable RouteTemplates.RouteTokenNotResolved

using Features.Authentications.Domain.Models.Authentications.SignIn;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;
using Constants = Shared.Core.Domain.Constants.Constants;

namespace Features.Authentications.Features.SignIn;

public class SignInController : PublicBaseController
{
    [HttpPost(Constants.Routes.Authentications.SignInByEmail)]
    public async Task<ActionResult<ApiResponse<SignInResponseDto>>> SignInByEmail([FromBody] SignInCommand requestDto)
    {
        var result = await Mediator.Send(requestDto);

        return Ok(ApiResponse.Ok(result));
    }
}
