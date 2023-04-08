using FluentValidation;

namespace Shared.Core.Domain.Extensions;

public static class StringValidator
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
}