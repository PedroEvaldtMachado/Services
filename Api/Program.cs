using Api.Domain;
using Api.Domain.Implementations;
using Api.Dtos;
using Api.Infra;
using System.Resources;

[assembly: NeutralResourcesLanguageAttribute("en")]

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddMvcCore()
    .AddApiExplorer()
    .AddNewtonsoftJson();

builder.Services.AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport();

builder.Services.AddCors(Services => Services.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();