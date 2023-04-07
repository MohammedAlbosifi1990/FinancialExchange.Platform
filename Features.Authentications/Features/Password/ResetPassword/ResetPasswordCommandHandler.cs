using Features.Authentications.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.Password.ResetPassword;

public sealed record
    ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer _localizer;
    private readonly AuthenticationsOption _authenticationsOption;

    public ResetPasswordCommandHandler(
        UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
        _authenticationsOption = authenticationsOption.Value;
    }

    public async Task<ApiResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.Id.ToString());
        if (user==null)
            throw NotFoundException.Throw(_localizer[AuthenticationsConst.UserNotExist]);

        if (user.IsDisabled)
            throw BadRequestException.Throw(_localizer[AuthenticationsConst.UserAccountIsDisabled]);

        if (user.IsValidConfirmationCode(command.Code, out var error))
            throw BadRequestException.Throw(_localizer[error]);

        
        var result =await _userManager.ChangePasswordAsync(user, user.HashPass.DecryptString(_authenticationsOption.HashPassKey),
            command.NewPassword);
        if (!result.Succeeded)
            throw BadRequestException.Throw("حدث خطأ أثناء محاولة تغير الرمز السري");

        user.HashPass = command.NewPassword.EncryptString(_authenticationsOption.HashPassKey);
        await _userManager.UpdateAsync(user);
        return ApiResult.Result();
    }
}