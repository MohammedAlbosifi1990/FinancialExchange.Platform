using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;

namespace Features.Authentications.Features.Test;
public sealed record TestCommand() : IRequest<bool>;

