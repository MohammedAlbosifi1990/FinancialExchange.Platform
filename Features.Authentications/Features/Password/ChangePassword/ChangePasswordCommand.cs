using Features.Authentications.Domain.Models.Authentications.ChangePassword;
using MediatR;

namespace Features.Authentications.Features.Password.ChangePassword;
public sealed record ChangePasswordCommand(Guid Id,string Password,string OldPassword) : IRequest<ChangePasswordResultDto>;

