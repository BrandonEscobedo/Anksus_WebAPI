using anskus.Domain.Cuestionarios;
using MediatR;

namespace anskus.Application.CuestionarioActivo.Querys.VerificarCodigoCuestionario
{
    public class VerificarCodigoHandler(ICuestionarioActivoRepository _repository) : IRequestHandler<VerificarCodigoQuery, bool>
    {
        public async Task<bool> Handle(VerificarCodigoQuery request, CancellationToken cancellationToken)
        {
            var response =await _repository.IsCodeValid(request.codigo);
            return response;
        }
    }
}
