using Features.Authentications.Domain.Models;
using MediatR;

namespace Features.Authentications.Features.Password.ForgetPassword;
public sealed record ForgetPasswordCommand(Guid Id) : IRequest<ApiResult>;

