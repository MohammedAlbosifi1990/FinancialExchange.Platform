namespace Features.Authentications.Domain.Models.Authentications.Register;

public class RegisterResultDto  
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public string? ConfirmationCode { get; set; }
    public Guid? UserId { get; set; }
    
    public static RegisterResultDto Failed(string error)
    {
        return new RegisterResultDto()
        {
            Message = error,
            IsSuccess = false
        };
    }
    public static RegisterResultDto SetSuccess(string code,Guid userId)
    {
        return new RegisterResultDto()
        {
            Message = null,
            IsSuccess = true,
            ConfirmationCode = code,
            UserId = userId
        };
    }
}

