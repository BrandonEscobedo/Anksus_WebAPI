using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain;
using anskus.Domain.Cuestionarios;
using MediatR;

namespace anskus.Application.Cuestionarios.Querys.GetById
{
    public sealed class GetCuestionarioByUserHandler : IRequestHandler<GetCuestionarioByUserQuery, List<CuestionarioDTO>>
    {
        public readonly ICuestionarioRepository _repository;
        public GetCuestionarioByUserHandler(ICuestionarioRepository repository)
        {
            _repository = repository;

        }
        public async Task<List<CuestionarioDTO>> Handle(GetCuestionarioByUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetbyUser(request.email);
            List<CuestionarioDTO> cuestionarios = new();
            if (response != null)
            {
                foreach (var cuestionarioResponse in response)
                {
                    var cuestionario = new CuestionarioDTO
                    {
                        Titulo = cuestionarioResponse.Titulo,
                        IdCuestionario = cuestionarioResponse.IdCuestionario

                    };
                    cuestionarios.Add(cuestionario);
                }

                return cuestionarios;

            }
            return null;

        }
    }


}
