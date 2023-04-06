using Features.Authentications.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Contract.Services.Sms;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Models;
using Shared.Core.Services.Emails;

namespace Features.Authentications.Features.ResendConfirmation;
public partial class ConfirmationController : PublicBaseController
{
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;

    public ConfirmationController(IEmailSender emailSender, ISmsSender smsSender)
    {
        _emailSender = emailSender;
        _smsSender = smsSender;
    }

    [HttpPost(Constants.Routes.Authentications.ResendConfirmationCode)]
    public async Task<ActionResult<ApiResponse>> ResendConfirmationCode(
        [FromBody] ResendConfirmationCommand requestDto)
    {
        var result = await Mediator.Send(requestDto);

        if (!result.IsSuccess)
            return Ok(ApiResponse.BadRequest(result.Message));

        if (requestDto.Type is CredentialType.EmailAndPassword or CredentialType.UserNameAndPassword)
            await _emailSender.SendEmail(requestDto.EmailOrPhone,result.Code!);
        else
            await _smsSender.SendSms(requestDto.EmailOrPhone,result.Code!);

        return Ok(ApiResponse.Ok(result));
    }
}