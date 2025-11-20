using FormularioPersonas;
using FormularioPersonas.Endpoints;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;



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
builder.Services.AddScoped<IRepositorioDirreciones, RepositorioDirreciones>();

builder.Services.AddAutoMapper(typeof(Program));

//fin de area de los servicios
var app = builder.Build();
//inicio de los middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseOutputCache();

app.MapGet("/",[EnableCors(policyName:"libre")] () => "Hello World!");
app.MapGroup("").MapPersonas();
app.MapGroup("").MapDirreciones();
//fin del area de los middleware
app.Run();

