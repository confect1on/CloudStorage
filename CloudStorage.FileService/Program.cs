using CloudStorage.FileService.Domain;
using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FilesService.BackgroundServices.OutboxPublisher;
using CloudStorage.FilesService.Infrastructure.CurrentUserAccessor;
using CloudStorage.FilesService.Infrastructure.OpenAPI;
using CloudStorage.Infrastructure.EventBus;
using CloudStorage.Infrastructure.Persistence.Extensions;
using CloudStorage.Infrastructure.S3Storage;
using CloudStorage.ServiceDefaults;
using CloudStorage.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.

builder.Services
    .AddHttpContextAccessor()
    .AddSingleton<ICurrentUserAccessor, CurrentUserAccessor>()
    .AddAsyncInitialization()
    .AddDomainServices()
    .AddPersistence(builder.Configuration)
    .AddS3Storage(builder.Configuration)
    .AddEventBus(builder.Configuration)
    .AddCloudStorageUseCases();

builder.Services
    .AddOutboxPublisher(builder.Configuration);
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
    .AddAuthentication()
    .AddKeycloakJwtBearer(
        "keycloak",
        "CloudStorage",
        options =>
        {
            options.RequireHttpsMetadata = false;
            options.Audience = "account";
        });
builder.Services.AddAuthorizationBuilder();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "Cloud Storage API"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateUp();
await app.InitAndRunAsync();
