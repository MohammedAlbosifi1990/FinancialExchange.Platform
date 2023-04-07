// ReSharper disable RouteTemplates.RouteTokenNotResolved

using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Contract.Services.Sms;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Registrations.PhoneAndPassword;

public class RegistrationController : PublicBaseController
{
    private readonly ISmsSender _smsSender;
    public RegistrationController(ISmsSender smsSender)
    {
        _smsSender = smsSender;
    }
    [HttpPost(RoutesConst.Authentications.RegisterByPhone)]
    public async Task<ActionResult<ApiResponse>> RegisterByPhone([FromBody] RegisterByPhoneCommand requestDto)
    {
        var result = await Mediator.Send(requestDto);

        if (!result.IsSuccess) return Ok(ApiResponse.BadRequest(result.Message));
        
        await _smsSender.SendSms(requestDto.Phone, result.ConfirmationCode!);
        return Ok(ApiResponse.Ok());
    }
}