using anskus.Application.Data;
using anskus.Domain;
using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain.Models.dbModels;
namespace anskus.Application.Cuestionarios.Commands.Create
{
    internal sealed class CreateCuestionarioCommandHandler : IRequestHandler<CreateCuestionarioCommand, int>
    {
        private readonly ICuestionarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCuestionarioCommandHandler(ICuestionarioRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentException(nameof(_repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(_unitOfWork));

        }

        public async Task<int> Handle(CreateCuestionarioCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Cuestionario cuestionario = new Cuestionario();
                cuestionario.Titulo = command.Cuestionario.Titulo;
                cuestionario.IdCategoria = 1;
                var response = await _repository.Add(cuestionario, command.email);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
    }
}
