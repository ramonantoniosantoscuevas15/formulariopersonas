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
            group.MapGet("/Obtener Dirrecion por id/{id:int}", ObtenerDirrecionPorId);
            group.MapPost("/Agregar Dirrecion", AgregarDirrecion);
            return group;
        }
        static async Task<Ok<List<DirrecionDTO>>> ObtenerDirreciones(IRepositorioDirreciones repositorio,IMapper mapper)
        {
            var dirreciones = await repositorio.ObtenerTodos();
            var dirrecionesDTO = mapper.Map<List<DirrecionDTO>>(dirreciones);
            return TypedResults.Ok(dirrecionesDTO);
        }
        static async Task<Results<Ok<DirrecionDTO>, NotFound>> ObtenerDirrecionPorId (IRepositorioDirreciones repositorio, int id,IMapper mapper)
        {
            var dirreciones = await repositorio.ObtenerPorId(id);
            if(dirreciones is null)
            {
                return TypedResults.NotFound();

            }
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirreciones);
            return TypedResults.Ok(dirrecionDTO);

        }
        static async Task<Created<DirrecionDTO>> AgregarDirrecion(CrearDirrecionDTO crearDirrecionDTO,IRepositorioDirreciones repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var dirreciones = mapper.Map<Dirreciones>(crearDirrecionDTO);
            var id = await repositorio.Crear(dirreciones);
            await outputCacheStore.EvictByTagAsync("dirreciones-get", default);
            var dirrecionDTO = mapper.Map<DirrecionDTO>(dirreciones);
            return TypedResults.Created($"/dirreciones/{id}", dirrecionDTO);

        }

    }
}
