using IzRoadbook.Extensions.Models;
using Krosoft.Extensions.WebApi.HealthChecks.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IzRoadbook.Extensions.Filters;

public class HealthChecksFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)

    {
        var pathItem = CreateOpenApiPathItem();

        openApiDocument.Paths.Add(Urls.Health.Check, pathItem);
        openApiDocument.Paths.Add(Urls.Health.Readiness, pathItem);
        openApiDocument.Paths.Add(Urls.Health.Liveness, pathItem);
    }

    private static OpenApiPathItem CreateOpenApiPathItem()
    {
        var response = new OpenApiResponse
        {
            Description = "Success"
        };
        response.Content.Add("application/json", new OpenApiMediaType

        {
            Schema = new OpenApiSchema
            {
                Type = "object",
                AdditionalPropertiesAllowed = true,
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "status", new OpenApiSchema { Type = "string" } }
                }
            }
        });

        var operation = new OpenApiOperation();
        operation.Tags.Add(new OpenApiTag { Name = "Health" });
        operation.Responses.Add("200", response);

        var pathItem = new OpenApiPathItem();
        pathItem.AddOperation(OperationType.Get, operation);
        return pathItem;
    }
}