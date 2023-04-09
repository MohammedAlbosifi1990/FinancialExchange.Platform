using Features.Offices.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.EditOffice;

public  record EditOfficeCommand(Guid OfficeId,EditOfficeRequestDto Dto) : IRequest<ApiResult<Office>>;

