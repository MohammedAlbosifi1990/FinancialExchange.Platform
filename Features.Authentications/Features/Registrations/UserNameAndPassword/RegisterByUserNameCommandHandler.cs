using System.Security.Claims;
using Features.Authentications.Domain.Models.Authentications.Register;
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

namespace Features.Authentications.Features.Registrations.UserNameAndPassword;

public sealed record
    RegisterByUserNameCommandHandler : IRequestHandler<RegisterByUserNameCommand,
        RegisterResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthenticationsOption _authenticationsOption;
    private readonly IStringLocalizer _localizer;

    public RegisterByUserNameCommandHandler(UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
        IStringLocalizer localizer)
    {
        _userManager = userManager;
        _authenticationsOption = authenticationsOption.Value;
        _localizer = localizer;
    }

    public async Task<RegisterResultDto> Handle(RegisterByUserNameCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager
            .Users
            .SingleOrDefaultAsync(u => u.UserName == command.UserName, cancellationToken);
        if (user != null)
            return RegisterResultDto.Failed(_localizer[AuthenticationsConst.UserNameIsAlreadyExist]);


        var registerUser = new User()
        {
            Email = command.Email,
            FullName = command.FullName,
            UserName = command.UserName,
            PhoneNumber = command.Phone,
            Type = UserType.None,
            ConfirmationCodeCreatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow,
            IsAccepted = _authenticationsOption.AutoAcceptable,
            HashPass = command.Password.EncryptString(_authenticationsOption.HashPassKey),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var code = registerUser.SetConfirmationCode();

        var result = await _userManager.CreateAsync(registerUser, command.Password);
        if (!result.Succeeded)
            return RegisterResultDto.Failed(result.Errors.FirstOrDefault()!.Description);

        var addRolesResult = await _userManager.AddToRoleAsync(registerUser, registerUser.Type.ToString());
        if (!addRolesResult.Succeeded)
            return RegisterResultDto.Failed(addRolesResult.Errors.FirstOrDefault()!.Description);
        var addClaimsResult = await AddClaims(registerUser);

        if (!addClaimsResult.Success)
            return RegisterResultDto.Failed(addClaimsResult.Error ?? "حدث خطأ أثناء أضافة المتطلبات ");

        return RegisterResultDto.SetSuccess(code, registerUser.Id);
    }

    private async Task<(bool Success, string? Error)> AddClaims(User registerUser)
    {
        var addClaimResults = await _userManager.AddClaimsAsync(registerUser, new List<Claim>()
        {
            new(ClaimsConst.UserId, registerUser.Id.ToString()),
            new(ClaimsConst.Name, registerUser.FullName),
            new(ClaimsConst.UserName, registerUser.UserName ?? ""),
            new(ClaimsConst.Email, registerUser.Email ?? ""),
            new(ClaimsConst.Phone, registerUser.PhoneNumber ?? ""),
            new(ClaimsConst.EmailConfirmed, registerUser.EmailConfirmed ? "true" : "false"),
            new(ClaimsConst.PhoneConfirmed, registerUser.PhoneNumberConfirmed ? "true" : "false"),
            new(ClaimsConst.Type, registerUser.Type.ToString()),
            new(ClaimsConst.IsDisabled, registerUser.IsDisabled ? "true" : "false"),
            new(ClaimsConst.IsAccepted, registerUser.IsAccepted ? "true" : "false")
            //TODO Add Permissions Claims
        });
        return addClaimResults.Succeeded
            ? (true, "")
            : (false, addClaimResults.Errors.First().Description);
    }
}