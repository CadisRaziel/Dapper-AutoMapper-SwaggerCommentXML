/*
using CrudDapperAndAutoMapper.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecao de dependencia
//IUsuarioInterface esta conectado com o UsuarioService
//Estamos avisando a interface que os metodos que ela tiver estarao implementados no usuarioService
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

//Configuracao do automapper
builder.Services.AddAutoMapper(typeof(Program)); //-> Program eu utilizo em toda a aplicacao

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

*/

using System.Reflection;
using CrudDapperAndAutoMapper.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    // Defina o caminho para o arquivo XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Injeção de dependência
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

