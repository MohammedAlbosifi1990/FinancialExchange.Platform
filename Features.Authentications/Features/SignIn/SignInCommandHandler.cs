using System.IdentityModel.Tokens.Jwt;
using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.SignIn;
using Features.Authentications.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Enum;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.SignIn;

public sealed record SignInCommandHandler : IRequestHandler<SignInCommand, SignInResultDto>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AuthenticationsOption _authenticationsOption;
    private readonly IStringLocalizer _localizer;

    public SignInCommandHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        ITokenService tokenService, IStringLocalizer localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authenticationsOption = authenticationsOption.Value;
        _tokenService = tokenService;
        _localizer = localizer;
    }

    public async Task<SignInResultDto> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = command.Type switch
        {
            CredentialType.EmailAndPassword => await _userManager.Users.SingleOrDefaultAsync(
                u => u.Email == command.Identity, cancellationToken),
            CredentialType.UserNameAndPassword => await _userManager.Users.SingleOrDefaultAsync(
                u => u.UserName == command.Identity, cancellationToken),
            CredentialType.PhoneAndPassword or CredentialType.PhoneAndOtpCode => await _userManager
                .Users.SingleOrDefaultAsync(u => u.PhoneNumber == command.Identity, cancellationToken),
            _ => null
        };

        if (user == null)
            return SignInResultDto.Failed(_localizer[AuthenticationsConst.UserNotExist]);

        if (user.IsDisabled)
        {
            await _userManager.AccessFailedAsync(user);
            return SignInResultDto.Failed(_localizer[AuthenticationsConst.UserAccountIsDisabled]);
        }

        if (await _userManager.IsLockedOutAsync(user))
            return SignInResultDto.Failed(string.Format(_localizer[AuthenticationsConst.UserAccountIsLocked],
                _authenticationsOption.LockoutTimeInMinute));


        switch (command.Type)
        {
            case CredentialType.EmailAndPassword when !user.EmailConfirmed:
                await _userManager.AccessFailedAsync(user);
                return SignInResultDto.Failed(_localizer[AuthenticationsConst.EmailIsNotConfirmed]);
            case CredentialType.PhoneAndPassword or CredentialType.PhoneAndOtpCode when !user.PhoneNumberConfirmed:
                await _userManager.AccessFailedAsync(user);
                return SignInResultDto.Failed(_localizer[AuthenticationsConst.PhoneIsNotConfirmed]);
        }


        if (command.Type == CredentialType.PhoneAndOtpCode)
        {
            if (user.ConfirmationCodeUsedFor != ConfirmationCodeFor.Phone)
            {
                await _userManager.AccessFailedAsync(user);
                return SignInResultDto.Failed(_localizer[AuthenticationsConst.ConfirmationCodeAssignedForPhone]);
            }

            if (!user.IsValidConfirmationCode(command.Secret, out var error))
            {
                await _userManager.AccessFailedAsync(user);
                return SignInResultDto.Failed(error);
            }
        }
        else
        {
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, command.Secret);
            if (!checkPasswordResult)
            {
                await _userManager.AccessFailedAsync(user);
                return SignInResultDto.Failed(_localizer[AuthenticationsConst.PasswordIsWrong]);
            }
        }

        var result = await _signInManager.PasswordSignInAsync(user,
            user.HashPass.DecryptString(_authenticationsOption.HashPassKey), false, true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                return SignInResultDto.Failed(string.Format(_localizer[AuthenticationsConst.UserAccountIsLocked],
                    _authenticationsOption.LockoutTimeInMinute));
            if (result.IsNotAllowed)
                return SignInResultDto.Failed(_localizer[AuthenticationsConst.UserAccountIsAllowed]);
        }

        var token = await _tokenService.GenerateAccessToken(user);
        var refreshToken = await _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_authenticationsOption.JwtOptions.RefreshTokenValidityInDays);
        await _userManager.UpdateAsync(user);

        return new SignInResultDto()
        {
            IsSuccess = true,
            IsDisabled = user.IsDisabled,
            IsAccepted = user.IsAccepted,
            UserId = user.Id,
            Name = user.FullName,
            ImagePath = user.ImagePath,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            CreatedAt = DateTime.UtcNow,
            ExpiredAt = token.ValidTo,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
        };
    }
}