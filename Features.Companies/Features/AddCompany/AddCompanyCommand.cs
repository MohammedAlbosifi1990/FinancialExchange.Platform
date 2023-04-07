using Features.Companies.Domain;
using MediatR;

namespace Features.Companies.Features.AddCompany;

public  record AddCompanyCommand(Guid UserId,string Name,string? Email) : IRequest<AddCompanyResultDto>;

