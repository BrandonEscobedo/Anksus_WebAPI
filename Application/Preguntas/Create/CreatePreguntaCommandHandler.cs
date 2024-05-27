using anskus.Domain.Cuestionarios;
using anskus.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anskus.Domain.Models.dbModels;

namespace anskus.Application.Preguntas.Create
{
    internal sealed class CreatePreguntaCommandHandler : IRequestHandler<CreatePreguntaCommand, int>
    {
        private readonly IPreguntasRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePreguntaCommandHandler(IPreguntasRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreatePreguntaCommand request, CancellationToken cancellationToken)
        {

            Pregunta pregunta = new Pregunta();
            pregunta.pregunta = request.preguntas.pregunta;
            pregunta.IdCuestionario = request.preguntas.IdCuestionario;

            var response = await _repository.Add(pregunta);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}
