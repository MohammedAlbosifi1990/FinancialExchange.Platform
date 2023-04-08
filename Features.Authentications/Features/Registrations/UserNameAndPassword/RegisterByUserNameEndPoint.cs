// ReSharper disable RouteTemplates.RouteTokenNotResolved

using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;
using Shared.Core.Services.Emails;

namespace Features.Authentications.Features.Registrations.UserNameAndPassword;

[Route(RoutesConst.Authentications.AuthPrefix)]
public class RegistrationController : PublicBaseController
{
    private readonly IEmailSender _emailSender;
    public RegistrationController(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
    [HttpPost(RoutesConst.Authentications.RegisterByUserName)]
    public async Task<ActionResult<ApiResponse>> RegisterByUserName([FromBody] RegisterByUserNameCommand requestDto)
    {
        var result = await Mediator.Send(requestDto);

        if (!result.IsSuccess) return Ok(ApiResponse.BadRequest(result.Message));
        
        await _emailSender.SendEmail(requestDto.Email, result.ConfirmationCode!);
        return Ok(ApiResponse.Ok());
    }
}