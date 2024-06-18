using InventoryManager.Api.Configuration;
using InventoryManager.Api.Services;
using InventoryManager.Database;
using InventoryManager.Domain.Configuration;
using InventoryManager.LabelPrinter;
using InventoryManager.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// TODO: Move the different service declarations to their respective projects in extension methods for IServiceCollection

builder.Services.AddDbContext<InventoryManagerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mssql")));

builder.Services.AddScoped<IStorageLocationService, StorageLocationService>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IStandardsService, StandardsService>();
builder.Services.AddScoped<ILabelDefinitionService, LabelDefinitionService>();

builder.Services.AddScoped<ILabelPrinterConfigurationService, LabelPrinterConfigurationService>();

builder.Services.AddScoped<IReportGenerator, ReportGenerator>();
// Inject the printer configuration from database
builder.Services.AddSingleton<IConfigureOptions<LabelPrinterConfiguration>, ConfigureLabelPrinterOptions>();

builder.Services.AddScoped<IPrintLabel, PrintLabel>();

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

WebApplication app = builder.Build();

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