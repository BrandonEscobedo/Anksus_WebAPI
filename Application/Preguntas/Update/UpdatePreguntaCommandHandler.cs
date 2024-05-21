using anskus.Domain.Cuestionarios;
using anskus.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using anskus.Domain.Models.dbModels;

namespace anskus.Application.Preguntas.Update
{
    internal sealed class UpdatePreguntaCommandHandler:IRequestHandler<UpdatePreguntaCommand,bool>
    {
        private readonly IPreguntasRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePreguntaCommandHandler(IPreguntasRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePreguntaCommand request, CancellationToken cancellationToken)
        {
            Pregunta pregunta = new();
            pregunta.pregunta = request.preguntas.pregunta;
            pregunta.IdCuestionario = request.preguntas.IdCuestionario;
            foreach(var resp in request.preguntas.Respuesta)
            {
              Respuesta respuesta = new Respuesta();
                respuesta.respuesta = resp.respuesta;
                respuesta.IdRespuesta = resp.IdRespuesta;
                respuesta.RCorrecta = resp.RCorrecta;
                pregunta.Respuesta.Add(respuesta);
            }
         
            var response = await _repository.UpdateAsnyc(pregunta);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
