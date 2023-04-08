using AutoMapper;
using Features.Offices.Domain;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.AddOffice;

public class OfficesController : PublicBaseController
{
    private readonly IMapper _mapper;

    public OfficesController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<ActionResult<ApiResponse<AddOfficeResponseDto>>> AddOffice([FromBody] AddOfficeRequestDto requestDto)
    {
        var result = await Mediator.Send(new AddOfficeCommand(requestDto));
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));

        var response= _mapper.Map<AddOfficeResponseDto>(result.Data);
        // var response= new AddOfficeResponseDto()
        // {
        //     Name = result.Data.Name,
        //     Address = result.Data.Address,
        //     Phone = result.Data.Phone,
        //     ReferenceNumber = result.Data.ReferenceNumber,
        //     Logo = result.Data.Logo,
        //     CompanyId = result.Data.CompanyId,
        //     Email = result.Data.Email,
        //     Longitude = result.Data.Longitude,
        //     CityId = result.Data.CityId,
        //     Latitude = result.Data.Latitude,
        //     WhatsAppPhone = result.Data.WhatsAppPhone,
        //     Balance = result.Data.Balance,
        //     IsDisabled = result.Data.IsDisabled,
        //     IsAccepted = result.Data.IsAccepted
        // };
        
        return Ok(ApiResponse<AddOfficeResponseDto>.Ok(response));
    }
}

