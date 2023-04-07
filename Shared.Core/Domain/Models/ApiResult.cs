using Microsoft.AspNetCore.Http;

namespace Shared.Core.Domain.Models;

public abstract class ApiResult
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }
    public int? StatusCode { get; set; } = StatusCodes.Status200OK;
}