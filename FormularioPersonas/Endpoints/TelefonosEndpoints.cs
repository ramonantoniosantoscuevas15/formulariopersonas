using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioPersonas.Endpoints
{
    public static class  TelefonosEndpoints
    {
        public static RouteGroupBuilder MapTelefonos(this RouteGroupBuilder group)
        {
            group.MapPost("/Agregar Telefono", AgregarTelefono);
            group.MapPut("/Actualizar Telefono/{id:int}", ActualizaTelefono);
            return group;
        }
        static async Task<Created<TelefonoDTO>> AgregarTelefono (CrearTelefonosDTO crearTelefonosDTO,
            IRepositorioTelefonos repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var telefonos = mapper.Map<Telefonos>(crearTelefonosDTO);
            var id =await repositorio.Crear(telefonos);
            await outputCacheStore.EvictByTagAsync("telefonos-get", default);
            var telefonoDTO = mapper.Map<TelefonoDTO>(telefonos);
            return TypedResults.Created($"/telefonos/{id}", telefonoDTO);
        }
        static async Task<Results<NoContent,NotFound>> ActualizaTelefono(int id,CrearTelefonosDTO crearTelefonosDTO,
            IOutputCacheStore outputCacheStore, IMapper mapper,IRepositorioTelefonos repositorio)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var telefonos = mapper.Map<Telefonos>(crearTelefonosDTO);
            telefonos.Id = await repositorio.Crear(telefonos);
            await repositorio.Actualizar(telefonos);
            await outputCacheStore.EvictByTagAsync("telefonos-get", default);
            return TypedResults.NoContent();

        }
    }
}
