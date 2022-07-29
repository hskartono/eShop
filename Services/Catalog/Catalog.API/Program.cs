using Catalog.API.Configuration;
using Catalog.Core.Interfaces;
using Catalog.Core.Services;
using Catalog.Infrastructure.Data;
using Infrastructure.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

Catalog.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
builder.Services.Configure<BaseUrlConfiguration>(configSection);
var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();

var serviceConfig = builder.Configuration.GetServiceConfig();
builder.Services.RegisterConsulServices(serviceConfig);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();


const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
        builder =>
        {
            //builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost").TrimEnd('/'));
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(CORS_POLICY);

app.UseAuthorization();

app.MapControllers();

app.Run();
