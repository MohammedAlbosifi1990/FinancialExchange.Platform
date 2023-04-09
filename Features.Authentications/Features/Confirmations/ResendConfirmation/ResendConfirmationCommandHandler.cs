using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Enum;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.Confirmations.ResendConfirmation;

public sealed record
    ResendConfirmationCommandHandler : IRequestHandler<ResendConfirmationCommand, ResendConfirmationCodeResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthenticationsOption _authenticationsOption;
    private readonly IStringLocalizer _localizer;

    public ResendConfirmationCommandHandler(UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _authenticationsOption = authenticationsOption.Value;
        _localizer = localizer;
    }

    public async Task<ResendConfirmationCodeResultDto> Handle(ResendConfirmationCommand command,
        CancellationToken cancellationToken)
    {
        var user = command.Type switch
        {
            CredentialType.EmailAndPassword => await _userManager.FindByEmailAsync(command.EmailOrPhone),
            CredentialType.UserNameAndPassword => await _userManager.FindByNameAsync(command.EmailOrPhone),
            CredentialType.PhoneAndPassword or CredentialType.PhoneAndOtpCode => await
                _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == command.EmailOrPhone,
                    cancellationToken: cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(CredentialType), command.Type, null)
        };

        if (user == null)
            throw BadRequestException.Throw(_localizer[AuthenticationsConst.UserNotExist]);

        if (user.IsDisabled)
        {
            await _userManager.AccessFailedAsync(user);
            throw BadRequestException.Throw(_localizer[AuthenticationsConst.UserAccountIsDisabled]);
        }

        if (await _userManager.IsLockedOutAsync(user))
            throw BadRequestException.Throw(_localizer[AuthenticationsConst.UserAccountIsLocked]);

        var code = user.SetConfirmationCode(
            command.Type switch
            {
                CredentialType.EmailAndPassword or CredentialType.UserNameAndPassword => ConfirmationCodeFor.Email,
                CredentialType.PhoneAndPassword or CredentialType.PhoneAndOtpCode => ConfirmationCodeFor.Phone,
                _ => throw BadRequestException.Throw("Some Error")

            }
        );
        
        await _userManager.UpdateAsync(user);
        return ResendConfirmationCodeResultDto.Success(code);
    }
}