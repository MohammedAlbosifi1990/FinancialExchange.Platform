using Features.Offices.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.RemoveOffice;

public  record RemoveOfficeCommand(Guid OfficeId) : IRequest<ApiResult>;

