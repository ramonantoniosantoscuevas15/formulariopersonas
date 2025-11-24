using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioPersonas.Endpoints
{
    public static class DirrecionesEndpoints
    {
        public static RouteGroupBuilder MapDirreciones(this RouteGroupBuilder group)
        {
           group.MapGet("/Obtener Dirreciones", ObtenerDirreciones).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("dirreciones-get"));
            group.MapGet("/Obtener Dirrecion por id/{id:int}", ObtenerDirrecionPorId).WithName("ObtenerDirrecionporid");
            group.MapPut("/Actualizar Dirrecion/{id:int}",ActualizarDirrecion);
            group.MapDelete("/Borrar Dirreciones/{id:int}",BorrarDirrecion);
            group.MapPost("/Agregar Dirrecion/persona/{personaId:int}/dirreciones", AgregarDirrecion);
            return group;
        }
        static async Task<Results<Ok<List<DirrecionDTO>>,NotFound>> ObtenerDirreciones(int personaId,IRepositorioDirreciones repositorioDirreciones,
            IMapper mapper,IRepositorioPersonas repositorioPersonas)
        {
            if (!await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
            var dirreciones = await repositorioDirreciones.ObtenerTodos(personaId);
            var dirrecionesDTO = mapper.Map<List<DirrecionDTO>>(dirreciones);
            return TypedResults.Ok(dirrecionesDTO);
        }
        static async Task<Results<Ok<DirrecionDTO>, NotFound>> ObtenerDirrecionPorId (int personaId,IRepositorioDirreciones repositorio, 
            int id,IMapper mapper)
        {
            var dirreciones = await repositorio.ObtenerPorId(id);
            if(dirreciones is null)
            {
                return TypedResults.NotFound();

            }
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirreciones);
            return TypedResults.Ok(dirrecionDTO);

        }
        static async Task<Results<NoContent,NotFound>> ActualizarDirrecion(int id, CrearDirrecionDTO crearDirrecionDTO,IRepositorioDirreciones
            repositorio,IMapper mapper,IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var dirreciones = mapper.Map<Dirreciones>(crearDirrecionDTO);
            dirreciones.Id = id;
            await repositorio.Actualizar(dirreciones);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            return TypedResults.NoContent();
        }
        static async Task<Results<NoContent,NotFound>> BorrarDirrecion (int id, 
            IRepositorioDirreciones repositorio,IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            return TypedResults.NoContent();
        }
        static async Task<Results <CreatedAtRoute<DirrecionDTO>,NotFound>> AgregarDirrecion(int personaId,CrearDirrecionDTO crearDirrecionDTO,IRepositorioDirreciones repositorioDirreciones,
            IRepositorioPersonas repositorioPersonas,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            if(! await repositorioPersonas.Existe(personaId))
            {
                return TypedResults.NotFound();
            }
           var dirrecion = mapper.Map<Dirreciones>(crearDirrecionDTO);
            dirrecion.PersonaId = personaId;
            var id = await repositorioDirreciones.Crear(dirrecion);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirrecion);
            return TypedResults.CreatedAtRoute(dirrecionDTO, "ObtenerDirrecionporid", new {id,personaId});

        }

    }
}
