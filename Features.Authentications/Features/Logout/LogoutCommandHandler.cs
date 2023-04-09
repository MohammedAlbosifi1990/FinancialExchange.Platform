using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Logout;

public sealed record
    LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IStringLocalizer _localizer;

    public LogoutCommandHandler(UserManager<User> userManager,
        IStringLocalizer localizer, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _localizer = localizer;
        _signInManager = signInManager;
    }
    public async Task<ApiResult> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user == null)
            throw NotFoundException.Throw(_localizer[AuthenticationsConst.UserNotExist]);

        user.ClearRefreshToken();
        await _userManager.UpdateAsync(user);
        await _signInManager.SignOutAsync();

        return ApiResult.Success();
    }
}