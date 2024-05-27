using anskus.Domain;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Update
{
    internal sealed class UpdateCuestionarioCommandHandler : IRequestHandler<UpdateCuestionarioCommand, bool>
    {
        private readonly ICuestionarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCuestionarioCommandHandler(ICuestionarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException(nameof(_repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(_unitOfWork));
        }

        public async Task<bool> Handle(UpdateCuestionarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Cuestionario cuestionario = new Cuestionario();
                cuestionario.IdCuestionario = request.Cuestionario.IdCuestionario;
                cuestionario.Titulo = request.Cuestionario.Titulo;
                cuestionario.Pregunta = (ICollection<Pregunta>)request.Cuestionario.Pregunta;
              
                var response = await _repository.Update(cuestionario);
                await _unitOfWork.SaveChangesAsync();
                return response;
            }
            catch
            {
                return false;
            }
        }
    }
}
