using Features.Offices.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.AddOffice;

public  record AddOfficeCommand(AddOfficeRequestDto Dto) : IRequest<ApiResult<Office>>;

