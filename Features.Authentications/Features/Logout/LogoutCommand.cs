using MediatR;
using Shared.Core.Domain.Models;

namespace Features.Authentications.Features.Logout;
public sealed record LogoutCommand(Guid UserId) : IRequest<ApiResult>;

