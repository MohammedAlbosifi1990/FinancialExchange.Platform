using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.CodeConfirmation;

public sealed record CodeConfirmationCommandHandler : IRequestHandler<CodeConfirmationCommand, ConfirmationOtpResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthenticationsOption _authenticationsOption;
    private readonly IStringLocalizer _localizer;

    public CodeConfirmationCommandHandler(UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _authenticationsOption = authenticationsOption.Value;
        _localizer = localizer;
    }

    public async Task<ConfirmationOtpResultDto> Handle(CodeConfirmationCommand command,
        CancellationToken cancellationToken)
    {
        var user = command.Type switch
        {
            CredentialType.EmailAndPassword => await _userManager.FindByEmailAsync(command.Identity),
            CredentialType.UserNameAndPassword => await _userManager.FindByNameAsync(command.Identity),
            CredentialType.PhoneAndPassword or CredentialType.PhoneAndOtpCode =>
                await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == command.Identity,
                    cancellationToken: cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(CredentialType), command.Type, null)
        };

        if (user == null)
            throw BadRequestException.Throw(_localizer[Constants.Authentications.UserNotExist]);

        if (user.IsDisabled)
            throw BadRequestException.Throw(_localizer[Constants.Authentications.UserAccountIsDisabled]);

        if (await _userManager.IsLockedOutAsync(user))
            throw BadRequestException.Throw(_localizer[Constants.Authentications.UserAccountIsLocked]);

        if (!user.IsValidConfirmationCode(command.Secret, out var error,
                _authenticationsOption.ConfirmationCodeExpirationTimeInMinute))
        {
            await _userManager.AccessFailedAsync(user);
            throw BadRequestException.Throw(_localizer[error]);
        }

        if (command.Type is CredentialType.EmailAndPassword or CredentialType.UserNameAndPassword)
            user.EmailConfirmed = true;
        else
            user.PhoneNumberConfirmed = true;

        await _userManager.UpdateAsync(user);
        return ConfirmationOtpResultDto.Success();
    }
}