using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioPersonas.Endpoints
{
    public static class CorreosEndpoints
    {
        public static RouteGroupBuilder MapCorreos(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Correos",ObtenerCorreos).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("correos-get"));
            group.MapPost("/Agregar Correos", AgregarCorreo);
            group.MapPut("/Actualizar Correos/{id:int}", ActualizarCorreos);
            group.MapDelete("/Eliminar Correos/{id:int}", BorrarCorreos);
            return group;
        }

        static async Task<Ok<List<CorreoDTO>>> ObtenerCorreos(IRepositorioCorreos repositorio,IMapper mapper)
        {
            var correos = await repositorio.ObtenerTodos();
            var correosDTO = mapper.Map<List<CorreoDTO>>(correos);
            return TypedResults.Ok(correosDTO);
        }

        static async Task<Created<CorreoDTO>> AgregarCorreo (CrearCorreosDTO crearCorreosDTO, IMapper mapper,IRepositorioCorreos repositorio,
            IOutputCacheStore outputCacheStore)
        {
            var correos = mapper.Map<Correos>(crearCorreosDTO);
            var id = repositorio.Crear(correos);
            await outputCacheStore.EvictByTagAsync("correos-get", default);
            var correoDTO = mapper.Map<CorreoDTO>(correos);
            return TypedResults.Created($"/correos/{id}",correoDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarCorreos(int id, CrearCorreosDTO crearCorreosDTO,IRepositorioCorreos repositorio,
            IOutputCacheStore outputCacheStore,IMapper mapper)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var correos = mapper.Map<Correos>(crearCorreosDTO);
            correos.Id = id;
            await repositorio.Actualizar(correos);
            await outputCacheStore.EvictByTagAsync("correos-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent,NotFound>> BorrarCorreos (int id,IRepositorioCorreos repositorio,
            IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("correos-get", default);
            return TypedResults.NoContent();

        }
    }
}
