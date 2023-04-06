using Features.Authentications.Domain.Models.Authentications.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.Password.ChangePassword;

public sealed record
    ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer _localizer;
    private readonly AuthenticationsOption _authenticationsOption;

    public ChangePasswordCommandHandler(
        UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _localizer = localizer;
        _authenticationsOption = authenticationsOption.Value;
    }

    public async Task<ChangePasswordResultDto> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.Id.ToString());
        if (user==null)
            throw NotFoundException.Throw(_localizer[Constants.Authentications.UserNotExist]);

        if (user.IsDisabled)
            throw BadRequestException.Throw(_localizer[Constants.Authentications.UserAccountIsDisabled]);

        if (!await _userManager.CheckPasswordAsync(user,command.OldPassword))
            throw BadRequestException.Throw("خطأ في رمز المرور القديم");
        
        var result =await _userManager.ChangePasswordAsync(user, user.HashPass.DecryptString(_authenticationsOption.HashPassKey),
            command.Password);
        if (!result.Succeeded)
            throw BadRequestException.Throw("حدث خطأ أثناء محاولة تغير الرمز السري");

        user.HashPass = command.Password.EncryptString(_authenticationsOption.HashPassKey);
        await _userManager.UpdateAsync(user);
        return ChangePasswordResultDto.Result();
    }
}