using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;

namespace FormularioPersonas.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //mapeando las personas
            CreateMap<CrearPersonaDTO, Persona>();
            CreateMap<Persona, PersonaDTO>();
            //mapeando las dirreciones
            CreateMap<CrearDirrecionDTO, Dirrecion>();
            CreateMap<Dirrecion, DirrecionDTO>();
            //mapeando los telefonos
            CreateMap<CrearTelefonosDTO, Telefonos>();
            CreateMap<Telefonos,TelefonoDTO>();
            //mapeando los correos
            CreateMap<CrearCorreosDTO, Correo>();
            CreateMap<Correo,CorreoDTO>();
            //mapeando las categorias
            CreateMap<CrearCategoriasDTO, Categoria>();
            CreateMap<Categoria,CategoriaDTO>();
        }
    }
}
