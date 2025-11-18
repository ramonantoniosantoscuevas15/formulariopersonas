using FormularioPersonas.Entidades;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//inicio de area de los servicios
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

//fin de area de los servicios
var app = builder.Build();
//inicio de los middleware

app.UseCors();

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
});

//fin del area de los middleware
app.Run();
