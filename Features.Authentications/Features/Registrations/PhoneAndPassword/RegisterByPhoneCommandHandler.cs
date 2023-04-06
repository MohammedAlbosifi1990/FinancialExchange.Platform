using System.Security.Claims;
using Features.Authentications.Domain.Models.Authentications.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Enum;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models.Options;
using Constants = Shared.Core.Domain.Constants.Constants;

namespace Features.Authentications.Features.Registrations.PhoneAndPassword;

public sealed record RegisterByPhoneCommandHandler : IRequestHandler<RegisterByPhoneCommand, RegisterResultDto>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthenticationsOption _authenticationsOption;
    private readonly IStringLocalizer _localizer;

    public RegisterByPhoneCommandHandler(UserManager<User> userManager,
        IOptions<AuthenticationsOption> authenticationsOption,
         IStringLocalizer localizer)
    {
        _userManager = userManager;
        _authenticationsOption = authenticationsOption.Value;
        _localizer = localizer;
    }

    public async Task<RegisterResultDto> Handle(RegisterByPhoneCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .Users
            .SingleOrDefaultAsync(u => u.PhoneNumber == command.Phone, cancellationToken);
        if (user != null)
            return RegisterResultDto.Failed(_localizer[Constants.Authentications.EmailIsAlreadyExist]);

       

        var registerUser = new User()
        {
            Email = command.Email,
            FullName = command.FullName,
            UserName = command.UserName,
            Type = UserType.None,
            PhoneNumber = command.Phone,
            ConfirmationCodeCreatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow,
            IsAccepted = _authenticationsOption.AutoAcceptable,
            HashPass = command.Password.EncryptString(_authenticationsOption.HashPassKey),
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var code = registerUser.SetConfirmationCode();

        var result = await _userManager.CreateAsync(registerUser, command.Password);
        if (!result.Succeeded)
            return RegisterResultDto.Failed(result.Errors.FirstOrDefault()!.Description);

        var addRolesResult = await _userManager.AddToRoleAsync(registerUser, registerUser.Type.ToString());
        if (!addRolesResult.Succeeded)
            return RegisterResultDto.Failed(addRolesResult.Errors.FirstOrDefault()!.Description);
        var addClaimsResult =await AddClaims(registerUser);
        
        if (!addClaimsResult.Success)
            return RegisterResultDto.Failed(addClaimsResult.Error??"حدث خطأ أثناء أضافة المتطلبات ");

        return RegisterResultDto.SetSuccess(code, registerUser.Id);
    }

    private async Task<(bool Success, string? Error)> AddClaims(User registerUser)
    {
        var addClaimResults = await _userManager.AddClaimsAsync(registerUser, new List<Claim>()
        {
            new(Constants.Claims.UserId, registerUser.Id.ToString()),
            new(Constants.Claims.Name, registerUser.FullName),
            new(Constants.Claims.UserName, registerUser.UserName ?? ""),
            new(Constants.Claims.Email, registerUser.Email ?? ""),
            new(Constants.Claims.Phone, registerUser.PhoneNumber ?? ""),
            new(Constants.Claims.EmailConfirmed, registerUser.EmailConfirmed ? "true" : "false"),
            new(Constants.Claims.PhoneConfirmed, registerUser.PhoneNumberConfirmed ? "true" : "false"),
            new(Constants.Claims.Type, registerUser.Type.ToString()),
            new(Constants.Claims.IsDisabled, registerUser.IsDisabled ? "true" : "false"),
            new(Constants.Claims.IsAccepted, registerUser.IsAccepted ? "true" : "false")
            //TODO Add Permissions Claims
        });
        return addClaimResults.Succeeded
            ? (true, "")
            : (false, addClaimResults.Errors.First().Description);
    }
}