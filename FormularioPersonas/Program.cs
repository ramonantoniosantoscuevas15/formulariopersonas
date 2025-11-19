using FormularioPersonas;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.OutputCaching;
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
app.MapGet("/Obtener personas", async(IRepositorioPersonas repositorio)  =>
{
   
    return await repositorio.ObtenerTodos();
}).CacheOutput(c=> c.Expire(TimeSpan.FromSeconds(60)).Tag("personas-get"));

app.MapGet("/Obtener personas por id/{id:int}", async (IRepositorioPersonas repositorio, int id) =>
{
    var persona = await repositorio.ObtenerPorId(id);
    if(persona is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(persona);

});

app.MapPost("/Agregar Personas", async (Personas personas, IRepositorioPersonas repositorio, IOutputCacheStore outputCacheStore) =>
{
    var id = await repositorio.Crear(personas);
    await outputCacheStore.EvictByTagAsync("personas-get",default);
    return Results.Created($"/personas/{id}", personas);
});

app.MapPut("/Actualizar Personas/{id:int}", async (int id, Personas personas, IRepositorioPersonas repositorio,
    IOutputCacheStore outputCacheStore) =>

 {
     var existe = await repositorio.Existe(id);
     if (!existe)
     {
         return Results.NotFound();
     }
     await repositorio.Actualizar(personas);
     await outputCacheStore.EvictByTagAsync("personas-get", default);
     return Results.NoContent();
 });

app.MapDelete("/Borrar Personas/{id:int}", async (int id, IRepositorioPersonas repositorio, IOutputCacheStore outputCacheStore) => 
{
    var existe = await repositorio.Existe(id);
    if (!existe)
    {
        return Results.NotFound();
    }
    await repositorio.Borrar(id);
    await outputCacheStore.EvictByTagAsync("personas-get", default);
    return Results.NoContent();


});

//fin del area de los middleware
app.Run();
