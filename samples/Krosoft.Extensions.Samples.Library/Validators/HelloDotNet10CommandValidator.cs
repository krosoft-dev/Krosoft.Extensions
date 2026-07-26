using FluentValidation;
using Krosoft.Extensions.Samples.Library.Models.Commands;

namespace Krosoft.Extensions.Samples.Library.Validators;

internal class HelloDotNet10CommandValidator : AbstractValidator<HelloDotNet10Command>
{
    public HelloDotNet10CommandValidator()
    { 
        RuleFor(c => c.Name).NotEmpty().NotNull();
    }
}