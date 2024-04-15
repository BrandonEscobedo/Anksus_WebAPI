    using Anksus_WebAPI.Models.dbModels;
    using anskus.Application.Data;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.Services;
using anskus.Domain.CuestionariosInterface;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    namespace anskus.Infrastructure.Repository.CuestionariosServices
    {
        internal sealed class CuestionarioRepository : ICuestionarioRepository
        {
            private readonly TestAnskusContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        public CuestionarioRepository(TestAnskusContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<GeneralResponse> Add(CuestionarioDTO cuestionario, string email)
            {
            var userid = userManager.FindByEmailAsync(email);
          
            Cuestionario cuestionario1 = new Cuestionario()
            {
                IdCategoria = 1,
                IdUsuario =userid.Id,
              Estado = false,
              Publico = false,
                Titulo = cuestionario.Titulo

                };

                _context.Cuestionarios.Add(cuestionario1);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Ok");
            }

            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Cuestionario>> GetAllAsync()
            {
                throw new NotImplementedException();
            }

            public Task<Cuestionario> GeyByIdAsync(int id)
            {
                throw new NotImplementedException();
            }

            public void Update(Cuestionario cuestionario)
            {
                throw new NotImplementedException();
            }
        }
    }
