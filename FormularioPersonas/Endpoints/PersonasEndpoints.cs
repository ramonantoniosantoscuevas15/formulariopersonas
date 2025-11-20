using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Migrations;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Personas = FormularioPersonas.Entidades.Personas;

namespace FormularioPersonas.Endpoints
{
    public static class PersonasEndpoints
    {
        public static RouteGroupBuilder MapPersonas(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener personas", ObtenerPersonas).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("personas-get"));

            group.MapGet("/Obtener persona por id/{id:int}", ObterpersonaPorId);

            group.MapPost("/Agregar Personas", AgregarPerosna);

            group.MapPut("/Actualizar Personas/{id:int}", ActualizarPersona);

            group.MapDelete("/Borrar Personas/{id:int}", BorrarPersona);
            return group;
        }
        static async Task<Ok<List<PersonaDTO>>> ObtenerPersonas(IRepositorioPersonas repositorio, IMapper mapper)
        {
            var personas = await repositorio.ObtenerTodos();
            var personasDTO = mapper.Map<List<PersonaDTO>>(personas);
            return TypedResults.Ok(personasDTO);
        }

        static async Task<Results<Ok<PersonaDTO>, NotFound>> ObterpersonaPorId(IRepositorioPersonas repositorio, int id, IMapper mapper)
        {
            var persona = await repositorio.ObtenerPorId(id);
            if (persona is null)
            {
                return TypedResults.NotFound();
            }
            var personaDTO = mapper.Map<PersonaDTO>(persona);
            return TypedResults.Ok(personaDTO);

        }

        static async Task<Created<PersonaDTO>> AgregarPerosna(CrearPersonaDTO CrearpersonaDTO, IRepositorioPersonas repositorio, 
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var personas = mapper.Map<Personas>(CrearpersonaDTO);
            var id = await repositorio.Crear(personas);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            var personaDTO = mapper.Map<PersonaDTO>(personas);
            return TypedResults.Created($"/personas/{id}", personaDTO);
        }

        static async Task<Results<NoContent, NotFound>> ActualizarPersona(int id, CrearPersonaDTO CrearpersonaDTO, IRepositorioPersonas repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var personas = mapper.Map<Personas>(CrearpersonaDTO);
            personas.Id = id;
            await repositorio.Actualizar(personas);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> BorrarPersona(int id, IRepositorioPersonas repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("personas-get", default);
            return TypedResults.NoContent();
        }

    }
}
