using Features.Authentications.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.Password.ForgetPassword;

public sealed record
    ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer _localizer;
    private readonly AuthenticationsOption _authenticationsOption;

    public ForgetPasswordCommandHandler(
        UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
        _authenticationsOption = authenticationsOption.Value;
    }

    public async Task<ApiResult> Handle(ForgetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.Id.ToString());
        if (user==null)
            throw NotFoundException.Throw(_localizer[Constants.Authentications.UserNotExist]);

        if (user.IsDisabled)
            throw BadRequestException.Throw(_localizer[Constants.Authentications.UserAccountIsDisabled]);

        user.SetConfirmationCode();
        await _userManager.UpdateAsync(user);
        return ApiResult.Result();
    }
}