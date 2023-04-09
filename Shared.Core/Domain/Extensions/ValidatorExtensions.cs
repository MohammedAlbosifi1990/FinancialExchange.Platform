using FluentValidation;
using static System.Double;

namespace Shared.Core.Domain.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> MustPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(phone => phone.IsValidPhone()).WithMessage("The Phone Is Not Valid");
    }

    public static IRuleBuilderOptions<T, string?> MustNullablePhone<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Must(phone => string.IsNullOrEmpty(phone) || phone.IsValidPhone())
            .WithMessage("The Phone Is Not Valid");
    }

    public static IRuleBuilderOptions<T, string> MustEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(email => email.IsValidEmail()).WithMessage("The Email Is Not Valid");
    }

    public static IRuleBuilderOptions<T, string?> MustNullableEmail<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Must(email => string.IsNullOrEmpty(email) || email.IsValidEmail())
            .WithMessage("The email Is Not Valid");
    }
    
    public static IRuleBuilderOptions<T, string> MustDouble<T>(this IRuleBuilder<T, string> ruleBuilder,string? name=null)
    {
        
        return ruleBuilder.Must(IsValidDouble)
            .WithMessage($"The {name ?? "Double"} Is Not Valid Double");
        
    }
    
    public static IRuleBuilderOptions<T, string?> MustNullAbleDouble<T>(this IRuleBuilder<T, string?> ruleBuilder,string? name=null)
    {
        return ruleBuilder.Must(IsValidNullAbleDouble)
            .WithMessage($"The {name ?? "Double"} Is Not Valid Double");
    }

    private static bool IsValidNullAbleDouble(this string? doubleVal)
    {
        if (string.IsNullOrEmpty(doubleVal))
            return true;
        TryParse(doubleVal, out var outVal);
        return !IsNaN(outVal) && !IsInfinity(outVal);
    }
    
    private static bool IsValidDouble(this string doubleVal)
    {
        if (string.IsNullOrEmpty(doubleVal))
            return false;
        TryParse(doubleVal, out var outVal);
        return !IsNaN(outVal) && !IsInfinity(outVal);
    }
    public static IRuleBuilderOptions<T, Guid> MustValidId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder.Must(id => id.NotEmpty())
            .WithMessage("The Id Is Not Valid");
    }
}