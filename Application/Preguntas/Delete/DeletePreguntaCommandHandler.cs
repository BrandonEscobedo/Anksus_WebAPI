using anskus.Domain;
using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Preguntas.Delete
{
    internal class DeletePreguntaCommandHandler : IRequestHandler<DeletePreguntaCommand, bool>
    {
        private readonly IPreguntasRepository _repository;
        private readonly IUnitOfWork _UnitOfWok;

        public DeletePreguntaCommandHandler(IPreguntasRepository repository, IUnitOfWork unitOfWok)
        {
            _repository = repository;
            _UnitOfWok = unitOfWok;
        }

        public async Task<bool> Handle(DeletePreguntaCommand request, CancellationToken cancellationToken)
        {
            var response =await _repository.Delete(request.id);
            await _UnitOfWok.SaveChangesAsync();
            return response;
        }
    }
}
