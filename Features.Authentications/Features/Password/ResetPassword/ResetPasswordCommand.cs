using Features.Authentications.Domain.Models;
using MediatR;

namespace Features.Authentications.Features.Password.ResetPassword;
public sealed record ResetPasswordCommand(Guid Id,string Code,string NewPassword) : IRequest<ApiResult>;

