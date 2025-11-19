using FormularioPersonas;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//inicio de area de los servicios
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnetion");
builder.Services.AddDbContext<AplicationDbContext>(opciones => opciones.UseNpgsql(connectionString));
builder.Services.AddCors(opciones => 
{
    opciones.AddDefaultPolicy
    (configuracion =>
    {
        configuracion.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });

    opciones.AddPolicy("libre", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioPersonas, RepositoriosPersonas>();

//fin de area de los servicios
var app = builder.Build();
//inicio de los middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseOutputCache();

app.MapGet("/",[EnableCors(policyName:"libre")] () => "Hello World!");
app.MapGet("/personas", () =>
{
    var personas = new List<Personas> 
    { 
        new Personas
        {
            Id = 1,
            Nombre ="Ramon",
            Apellido ="Santos",
            Cedula="402-255513-38"
            
        },
         new Personas
        {
            Id = 2,
            Nombre ="Rosa",
            Apellido ="Martinez",
            Cedula="402-255513-38",
            

        }

    };
    return personas;
}).CacheOutput(c=> c.Expire(TimeSpan.FromSeconds(15)));

app.MapPost("/Agregar Personas", async (Personas personas, IRepositorioPersonas repositorio) =>
{
    var id = await repositorio.Crear(personas);
    return Results.Created($"/personas/{id}", personas);
});

//fin del area de los middleware
app.Run();
