using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Enum;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models.Options;

namespace Features.Authentications.Features.Test;

public sealed record
    TestCommandHandler : IRequestHandler<TestCommand, bool>
{
    public Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}