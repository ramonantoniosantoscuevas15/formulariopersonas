using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Migrations;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Persona = FormularioPersonas.Entidades.Persona;

namespace FormularioPersonas.Endpoints
{
    public static class PersonasEndpoints
    {
        public static RouteGroupBuilder MapPersonas(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener personas", ObtenerPersonas).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("personas-get"));

            group.MapGet("/Obtener persona por id/{id:int}", ObterpersonaPorId);

            group.MapGet("/Busqueda por nombre",BusquedaPorNombre);

            group.MapPost("/Agregar Personas", AgregarPersona);

            group.MapPut("/Actualizar Personas/{id:int}", ActualizarPersona);

            group.MapDelete("/Borrar Personas/{id:int}", BorrarPersona);
            return group;
        }
        static async Task<Ok<List<PersonaDTO>>> ObtenerPersonas(IRepositorioPersonas repositorio, IMapper mapper,
            int pagina =1, int recordsPorPagina = 10)
        {
            var paginacion = new PaginacionDTO { Pagina = pagina , RecordsPorPagina = recordsPorPagina};
            var personas = await repositorio.ObtenerTodos(paginacion);
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

        static async Task<Created<PersonaDTO>> AgregarPersona(CrearPersonaDTO CrearpersonaDTO, IRepositorioPersonas repositorio, 
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var personas = mapper.Map<Persona>(CrearpersonaDTO);
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
            var personas = mapper.Map<Persona>(CrearpersonaDTO);
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

        static async Task<Ok<List<PersonaDTO>>> BusquedaPorNombre(string nombre, IRepositorioPersonas repositorioPersonas,IMapper mapper)
        {
            var persona = await repositorioPersonas.BusquedaPorNombre(nombre);
            var personaDTO = mapper.Map<List<PersonaDTO>>(persona);
            return TypedResults.Ok(personaDTO);
        }

    }
}
