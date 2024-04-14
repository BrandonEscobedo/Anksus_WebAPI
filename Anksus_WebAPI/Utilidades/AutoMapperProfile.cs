using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using AutoMapper;

namespace Anksus_WebAPI.Server.Utilidades
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            //CreateMap<Pregunta, PreguntasDTO>().ForMember(e=>e.Pregunta,x=>x.MapFrom(d=>d.Pregunta1));
            //CreateMap<Cuestionario, CuestionarioDTO>();
            //CreateMap<Respuesta, RespuestasDTO>().ForMember(x => x.respuesta, d => d.MapFrom(x => x.Respuesta1))
            //    .ReverseMap()
            //    .ForMember(x=>x.Respuesta1,d=>d.MapFrom(x=>x.respuesta));
        }
    }
}
