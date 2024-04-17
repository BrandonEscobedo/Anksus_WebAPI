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
using Anksus_WebAPI.Models.dbModels;
using anskus.Application.DTOs.Cuestionarios;
namespace anskus.Application.Cuestionarios.Create
{
    internal sealed class CreateCuestionarioCommandHandler : IRequestHandler<CreateCuestionarioCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ICuestionarioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCuestionarioCommandHandler(ICuestionarioRepository repository, IUnitOfWork mediator, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentException(nameof(_repository));
            _unitOfWork = mediator ?? throw new ArgumentException(nameof(_unitOfWork));
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateCuestionarioCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Cuestionario cuestionario = new Cuestionario();
                cuestionario.Titulo = command.Cuestionario.Titulo;
                cuestionario.IdCategoria = command.Cuestionario.IdCategoria;
                cuestionario.IdUsuario = command.Cuestionario.IdUsuario;        
               var response = await _repository.Add(cuestionario);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
              
                return response;
            }
                catch (Exception ex )
            {

                return 0;
            }
        }
    }
}
