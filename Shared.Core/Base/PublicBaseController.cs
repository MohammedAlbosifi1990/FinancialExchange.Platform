using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Domain.Constants;

namespace Shared.Core.Base;

[ApiController]
[Route(RoutesConst.PublicPrefixC)]
public abstract class PublicBaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}

[ApiController]
[Route(RoutesConst.ProtectedPrefixC)]
public abstract class ProtectedBaseApiController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

}