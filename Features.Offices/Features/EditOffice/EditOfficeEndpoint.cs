using AutoMapper;
using Features.Offices.Domain;
using Features.Offices.Features.AddOffice;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Base;
using Shared.Core.Domain.Models;

namespace Features.Offices.Features.EditOffice;

public class OfficesController : PublicBaseController
{
    private readonly IMapper _mapper;

    public OfficesController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<OfficeResponseDto>>> EditOffice(Guid id,[FromForm] EditOfficeRequestDto requestDto)
    {
        var result = await Mediator.Send(new EditOfficeCommand(id,requestDto));
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));

        var response= _mapper.Map<OfficeResponseDto>(result.Data);
        return Ok(ApiResponse<OfficeResponseDto>.Ok(response));
    }
}

