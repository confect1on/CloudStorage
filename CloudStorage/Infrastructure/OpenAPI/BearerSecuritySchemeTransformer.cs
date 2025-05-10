using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace CloudStorage.Infrastructure.OpenAPI;

internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.All(authScheme => authScheme.Name != JwtBearerDefaults.AuthenticationScheme))
        {
            return;
        }
        var requirements = new Dictionary<string, OpenApiSecurityScheme>
        {
            [JwtBearerDefaults.AuthenticationScheme] = new()
            {
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
                BearerFormat = "Json Web Token"
            }
        };
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = requirements;
    }
}
