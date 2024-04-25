using anskus.Domain;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using MediatR;

namespace anskus.Application.Respuestas.Create
{
    internal sealed class CreateRespuestasCommandHandler : IRequestHandler<CreateRespuestasCommand, bool>
    {
        private readonly IRespuestasRepository _repository;
        private readonly IUnitOfWork _UnitOfWork;

        public CreateRespuestasCommandHandler(IRespuestasRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _UnitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateRespuestasCommand request, CancellationToken cancellationToken)
        {
            List<Respuesta> respuestas = new();
            foreach(var respuestaDto in request.respuestas)
            {
                var respuesta = new Respuesta
                {
                    respuesta = respuestaDto.respuesta,
                    RCorrecta = respuestaDto.RCorrecta,
                    IdPregunta=respuestaDto.IdPregunta
                };

                respuestas.Add(respuesta);
            }
         var result=   await _repository.Add(respuestas);
            await _UnitOfWork.SaveChangesAsync();
            return result;

        }
    }
}
