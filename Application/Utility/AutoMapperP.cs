using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain.Models.dbModels;
using AutoMapper;

namespace anskus.Application.Utility
{
    public class AutoMapperP : Profile
    {
        public AutoMapperP() 
            
        {
            CreateMap<Pregunta, PreguntasDTO>().ReverseMap();
            CreateMap<CuestionarioDTO, Cuestionario>().ReverseMap();
            CreateMap<Respuesta, RespuestasDTO>()
                .ReverseMap();
            CreateMap<anskus.Domain.Models.dbModels.CuestionarioActivo, CuestionarioActivoDTO>().ReverseMap();
        }

        
    }
}
