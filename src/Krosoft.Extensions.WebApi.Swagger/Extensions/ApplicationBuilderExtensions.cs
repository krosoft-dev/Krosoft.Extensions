using Microsoft.AspNetCore.Builder;

namespace Krosoft.Extensions.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerExt(this IApplicationBuilder builder,
                                                    bool useSwagger)
    {
        if (useSwagger)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }

        return builder;
    }
}