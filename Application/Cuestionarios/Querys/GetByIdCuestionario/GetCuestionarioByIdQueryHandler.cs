using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain.Cuestionarios;
using MediatR;
using AutoMapper;
using anskus.Domain.Models.dbModels;
namespace anskus.Application.Cuestionarios.Querys.GetByIdCuestionario
{
    public class GetCuestionarioByIdQueryHandler(ICuestionarioRepository _repository
        ,IMapper mapper) : IRequestHandler<GetCuestionarioByIdQuery, CuestionarioDTO>
    {

        public async Task<CuestionarioDTO> Handle(GetCuestionarioByIdQuery request, CancellationToken cancellationToken)
        {
            Cuestionario response = await _repository.GetbyId(request.id);
            if (response != null)
            {
               var result = mapper.Map<CuestionarioDTO>(response);
                return result;
            }
            return default!;

        }
    }
}
