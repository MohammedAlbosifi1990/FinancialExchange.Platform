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
    public async Task<ActionResult<ApiResponse<OfficeResponseDto>>> AddOffice([FromForm] AddOfficeRequestDto requestDto)
    {
        var result = await Mediator.Send(new AddOfficeCommand(requestDto));
        if (!result.IsSuccess)
            return BadRequest(ApiResponse.BadRequest(result.Message));

        var response= _mapper.Map<OfficeResponseDto>(result.Data);
        return Ok(ApiResponse<OfficeResponseDto>.Ok(response));
    }
}

