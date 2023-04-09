using AutoMapper;
using Features.Offices.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Base;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Offices.Features;

public class OfficesController : PublicBaseController
{
    private readonly IMapper _mapper;
    private readonly IRepository<Office> _officeRepo;

    public OfficesController(IMapper mapper, IRepository<Office> officeRepo)
    {
        _mapper = mapper;
        _officeRepo = officeRepo;
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<OfficeResponseDto>>> GetById(Guid id)
    {
        var office = await _officeRepo.SingleOrDefault(o => o.Id == id);
        if (office == null)
            throw NotFoundException.Throw("Office Not Found");
        var response= _mapper.Map<OfficeResponseDto>(office);

        return Ok(ApiResponse<OfficeResponseDto>.Ok(response));
    }
    
    [HttpGet("ByPhone")]
    public async Task<ActionResult<ApiResponse<OfficeResponseDto>>> GetByPhone(string phone)
    {
        if (!phone.IsValidPhone())
            throw InvalidArgumentException.Throw("Phone Number Is Not Valid");
        var office = await _officeRepo.SingleOrDefault(o => o.Phone == phone || o.WhatsAppPhone==phone);
        if (office == null)
            throw NotFoundException.Throw("Office Not Found");
        var response= _mapper.Map<OfficeResponseDto>(office);

        return Ok(ApiResponse<OfficeResponseDto>.Ok(response));
    }
    [HttpGet("ByReferenceNumber")]
    public async Task<ActionResult<ApiResponse<OfficeResponseDto>>> ByReferenceNumber(string referenceNumber)
    {
        if (string.IsNullOrEmpty(referenceNumber))
            throw InvalidArgumentException.Throw("Reference Number Is Not Valid");
        
        var office = await _officeRepo.SingleOrDefault(o => o.ReferenceNumber == referenceNumber);
        if (office == null)
            throw NotFoundException.Throw("Office Not Found");
        var response= _mapper.Map<OfficeResponseDto>(office);

        return Ok(ApiResponse<OfficeResponseDto>.Ok(response));
    }
    
    [HttpGet("Search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OfficeResponseDto>>>> Search(string searchVal,Guid? companyId)
    {
        var queryable = await _officeRepo.AsQueryable();
        if (companyId != null && companyId != Guid.Empty)
            queryable = queryable.Where(o => o.CompanyId == companyId);
        
        
        queryable=queryable
            .Include(o=>o.City)
            .Include(o=>o.Company)
            .Where(o=>
            o.Name.Contains(searchVal) || 
            o.Phone.Contains(searchVal) ||
            (!string.IsNullOrEmpty(o.Email) && o.Email.Contains(searchVal)) ||
            (!string.IsNullOrEmpty(o.WhatsAppPhone) && o.WhatsAppPhone.Contains(searchVal)) ||
            o.ReferenceNumber.Contains(searchVal) ||
            o.Address.Contains(searchVal) 
            );
        
        var offices = await queryable.ToListAsync();
        var response= _mapper.Map<IEnumerable<OfficeResponseDto>>(offices);

        return Ok(ApiResponse<IEnumerable<OfficeResponseDto>>.Ok(response));
    }
}

