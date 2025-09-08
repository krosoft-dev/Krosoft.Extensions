using FluentValidation;

namespace Krosoft.Extensions.Validations.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
                                                                     params TProperty[] validOptions)
    {
        if (validOptions == null || validOptions.Length == 0)
        {
            throw new ArgumentException("At least one valid option is required.", nameof(validOptions));
        }

        var formattedOptions = FormatValidOptions(validOptions);

        return ruleBuilder
               .Must(value => validOptions.Contains(value))
               .WithMessage($"'{{PropertyName}}' must be one of these values: {formattedOptions}");
    }

 

    private static string FormatValidOptions<TProperty>(TProperty[] validOptions)
    {
        return validOptions.Length switch
        {
            1 => validOptions[0]?.ToString() ?? "null",
            2 => $"{validOptions[0]} or {validOptions[1]}",
            _ => $"{string.Join(", ", validOptions.Take(validOptions.Length - 1).Select(vo => vo?.ToString()))} or {validOptions.Last()}"
        };
    }
}