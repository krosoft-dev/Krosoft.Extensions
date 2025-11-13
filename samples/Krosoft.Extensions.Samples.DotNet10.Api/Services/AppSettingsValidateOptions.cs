using FluentValidation;
using Krosoft.Extensions.Options.Services;
using Krosoft.Extensions.Samples.DotNet10.Api.Models;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Services;

internal class AppSettingsValidateOptions(IValidator<AppSettings> validator) : SettingsValidateOptions<AppSettings>(validator);

 