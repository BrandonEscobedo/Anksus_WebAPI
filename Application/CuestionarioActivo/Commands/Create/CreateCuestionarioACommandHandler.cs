using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain;
using anskus.Domain.Cuestionarios;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Commands.Create
{
    public class CreateCuestionarioACommandHandler(ICuestionarioActivoRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCuestionarioActivoCommand, CuestionarioActivoDTO>
    {

        public async Task<CuestionarioActivoDTO> Handle(CreateCuestionarioActivoCommand request, CancellationToken cancellationToken)
        {
            var response = await repository.ActivarCuestionario(request.idcuestionario, request.email);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<CuestionarioActivoDTO>(response);
            return result;
        }
    }
}

