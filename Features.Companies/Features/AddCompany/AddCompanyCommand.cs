using Features.Companies.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Companies.Features.AddCompany;

public  record AddCompanyCommand(Guid UserId,string Name,string? Email) : IRequest<ApiResult<Company>>;

