using AutoMapper;
using FormularioPersonas.DTOs;
using FormularioPersonas.Entidades;

namespace FormularioPersonas.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearPersonaDTO, Personas>();
            CreateMap<Personas, PersonaDTO>();
        }
    }
}
