using FluentValidation;

namespace Krosoft.Extensions.Validations.Extensions;

public static class ValidatorExtensions
{
    public static async Task<ISet<string>> ValidateAsync<T>(this IEnumerable<IValidator<T>> validators,
                                                            T request,
                                                            CancellationToken cancellationToken)
    {
        var failures = new List<string>();
        foreach (var validator in validators)
        {
            await validator.ValidateWithErrorMessageAsync(request, f => { failures.AddRange(f); }, cancellationToken);
        }

        return failures.ToHashSet();
    }

    public static async Task ValidateWithErrorMessageAsync<T>(this IValidator<T> validator,
                                                              T request,
                                                              Action<IEnumerable<string>> action,
                                                              CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult != null)
        {
            action(validationResult.Errors.Select(er => er.ErrorMessage).ToHashSet());
        }
    }
}