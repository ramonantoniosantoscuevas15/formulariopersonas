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
            CreateMap<CrearPersonaDTO, Personas>();
            CreateMap<Personas, PersonaDTO>();
            //mapeando las dirreciones
            CreateMap<CrearDirrecionDTO, Dirreciones>();
            CreateMap<Dirreciones, DirrecionDTO>();
            //mapeando los telefonos
            CreateMap<CrearTelefonosDTO, Telefonos>();
            CreateMap<Telefonos,TelefonoDTO>();
        }
    }
}
