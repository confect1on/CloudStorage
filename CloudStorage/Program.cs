using CloudStorage.Domain;
using CloudStorage.Infrastructure.DataAccess.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainServices();
builder.Services.AddDataAccessInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MigrateUp();
await app.RunAsync();
