using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
using AutoMapper;

namespace anskus.Application.Utility
{
    public class AutoMapperIns:Profile
    {
        public AutoMapperIns() 
            
        {
            CreateMap<Pregunta, PreguntasDTO>().ReverseMap();
            CreateMap<CuestionarioDTO, Cuestionario>().ReverseMap();
            CreateMap<Respuesta, RespuestasDTO>()
                .ReverseMap();
        }

        
    }
}
