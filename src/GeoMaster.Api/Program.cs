using GeoMaster.Application.Abstractions;
using GeoMaster.Application.Factory;
using GeoMaster.Application.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GeoMaster API (CP4)",
        Version = "v1",
        Description = "API de c�lculos geom�tricos e valida��o de conten��o"
    });

    // Inclui todos os .xml dispon�veis (Api/Application/Domain), se existirem
    foreach (var xml in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml"))
        c.IncludeXmlComments(xml, includeControllerXmlComments: true);
});

// Inje��o de depend�ncias como Singleton
builder.Services.AddSingleton<ICalculadoraService, CalculadoraService>();
builder.Services.AddSingleton<IFormaFactory, FormaFactory>();

builder.Services.AddSingleton<IValidacoesService, ValidacoesService>();
builder.Services.AddSingleton<IFormaContivelFactory, FormaContivelFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(configurationSwagger =>
    {
        configurationSwagger.SwaggerEndpoint("/swagger/v1/swagger.json", "API CP4 GeoMaster v1");
        configurationSwagger.RoutePrefix = string.Empty; //Define que o swagger UI seja a p�gina inicial
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();