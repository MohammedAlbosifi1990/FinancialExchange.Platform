using Features.Authentications.Domain.Models.Authentications.Confirmations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.CodeConfirmation;
[Route(RoutesConst.Authentications.AuthPrefix)]
public partial class ConfirmationController : PublicBaseController
{
    /// <summary>
    /// Creates a TodoItem.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>A newly created TodoItem</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///        "id": 1,
    ///        "name": "Item #1",
    ///        "isComplete": true
    ///     }
    [HttpPost(RoutesConst.Authentications.Confirm)]
    public async Task<ActionResult<ApiResponse>> SignInByEmail([FromBody] CodeConfirmationCommand requestDto)
    {
        IApiResult result = await Mediator.Send(requestDto);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));
        return Ok(ApiResponse.Ok(result.Data));
    }
}