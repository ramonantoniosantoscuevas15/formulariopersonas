using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;
using FormularioPersonas.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace FormularioPersonas.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static RouteGroupBuilder MapCategorias(this RouteGroupBuilder group)
        {
            group.MapGet("/Obtener Categorias",ObtenerCategorias).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("categorias-get"));
            group.MapPost("/Agregar Categorias",AgregarCategorias);
            group.MapPut("/Actualizar Categorias/{id:int}", ActualizarCategoria);
            group.MapDelete("/Borrar Categorias/{id:int}",BorrarCategorias);
            return group;
        }
        static async Task<Ok<List<CategoriaDTO>>> ObtenerCategorias(IRepositorioCategorias repositorio,IMapper mapper)
        {
            var categorias = await repositorio.ObtenerTodos();
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);
            return TypedResults.Ok(categoriasDTO);
        }
        static async Task<Created<CategoriaDTO>> AgregarCategorias(CrearCategoriasDTO crearCategoriasDTO,IRepositorioCategorias repositorio,
            IMapper mapper, IOutputCacheStore outputCacheStore)
        {
            var categorias = mapper.Map<Categoria>(crearCategoriasDTO);
            var id = await repositorio.Crear(categorias);
            await outputCacheStore.EvictByTagAsync("categorias-get", default);
            var categoriaDTO = mapper.Map<CategoriaDTO>(categorias);
            return TypedResults.Created($"/categorias/{id}",categoriaDTO);

        }
        static async Task<Results<NoContent,NotFound>> ActualizarCategoria(int id, CrearCategoriasDTO crearCategoriasDTO,IRepositorioCategorias repositorio,
            IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            var categorias = mapper.Map<Categoria>(crearCategoriasDTO);
            categorias.Id = id;
            await repositorio.Actualizar(categorias);
            await outputCacheStore.EvictByTagAsync("categorias-get", default);
            return TypedResults.NoContent();
        }
        static async Task<Results<NoContent, NotFound>> BorrarCategorias(int id,IRepositorioCategorias repositorio, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            await repositorio.Borrar(id);
            await outputCacheStore.EvictByTagAsync("categorias-get", default);
            return TypedResults.NoContent();
        }
    }
}
