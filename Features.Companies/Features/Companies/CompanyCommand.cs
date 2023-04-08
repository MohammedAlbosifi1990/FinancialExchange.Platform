using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Companies.Features.Companies;

public  record CompanyCommand(Guid UserId,string Name,string? Email) : IRequest<ApiResult<Company>>;

