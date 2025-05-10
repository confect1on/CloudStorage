using CloudStorage.Domain;
using CloudStorage.Infrastructure;
using CloudStorage.Infrastructure.DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDomainServices()
    .AddDataAccessInfrastructure(builder.Configuration);
builder.Services
    .AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi(opt =>
    {
        opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        opt.AddOperationTransformer<AuthorizeOperationTransformer>();
    });
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        jwtOptions =>
        {
            jwtOptions.Audience = builder.Configuration["Jwt:Audience"];
            jwtOptions.Authority = builder.Configuration["Jwt:Authority"];
        });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "Cloud Storage API"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateUp();
await app.RunAsync();
