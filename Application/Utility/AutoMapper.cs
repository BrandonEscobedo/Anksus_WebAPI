using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain.Models.dbModels;
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
