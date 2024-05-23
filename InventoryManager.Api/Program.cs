using InventoryManager.Api.Services;
using InventoryManager.Database;
using InventoryManager.Reports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<InventoryManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mssql")));

builder.Services.AddScoped<IStorageCaseService, StorageCaseService>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IStandardsService, StandardsService>();
builder.Services.AddScoped<IReportGenerator, ReportGenerator>();

builder.Services.AddControllers();

builder.Services.AddOpenApiDocument();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "Development", policy =>
        {
            policy.WithOrigins("http://localhost:4200");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();