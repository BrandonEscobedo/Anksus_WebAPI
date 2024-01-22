using Anksus_WebAPI.Models.dbModels;
using Anksus_WebAPI.Models.DTO;
using AutoMapper;

namespace Anksus_WebAPI.Server.Utilidades
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Pregunta, PreguntasDTO>().ForMember(e=>e.Pregunta,x=>x.MapFrom(d=>d.Pregunta1));
        }
    }
}
