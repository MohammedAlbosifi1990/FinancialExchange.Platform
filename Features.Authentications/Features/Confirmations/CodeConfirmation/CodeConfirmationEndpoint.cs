using Features.Authentications.Domain.Models.Authentications.Confirmations;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Confirmations.CodeConfirmation;
[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class ConfirmationController : PublicBaseController
{
   
    [HttpPost(RoutesConst.Authentications.Confirm)]
    public async Task<ActionResult<ApiResponse>> SignInByEmail([FromBody] CodeConfirmationCommand requestDto)
    {
        IApiResult result = await Mediator.Send(requestDto);
        return !result.IsSuccess 
            ? BadRequest(ApiResponse.BadRequest(result.Message)) 
            : (ActionResult<ApiResponse>)Ok(ApiResponse.Ok(result.Data));
    }
}