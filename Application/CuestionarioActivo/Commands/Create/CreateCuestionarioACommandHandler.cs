using anskus.Domain;
using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Commands.Create
{
    public class CreateCuestionarioACommandHandler(ICuestionarioActivoRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateCuestionarioActivoCommand, int>
    {

        public async Task<int> Handle(CreateCuestionarioActivoCommand request, CancellationToken cancellationToken)
        {
            int response = await repository.ActivarCuestionario(request.idcuestionario, request.email);
            await unitOfWork.SaveChangesAsync();
            return response;
        }
    }
}

