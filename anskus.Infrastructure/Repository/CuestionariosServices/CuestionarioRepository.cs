 using Anksus_WebAPI.Models.dbModels; 
using anskus.Application.Data;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Application.DTOs.Response;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Services;
using anskus.Domain.Cuestionarios;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    namespace anskus.Infrastructure.Repository.CuestionariosServices
    {
        internal sealed class CuestionarioRepository : ICuestionarioRepository
        {
        
        private readonly TestAnskusContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        public CuestionarioRepository(TestAnskusContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context ?? throw new ArgumentException(nameof(_context));
            this.userManager = userManager;
            
        }
        public async Task<int> Add(Cuestionario cuestionario)
            {
            //var userid = userManager.FindByEmailAsync(cuestionario!) ?? throw new Exception("NO se encontro el usuario");
             _context.Cuestionarios.Add(cuestionario);
            await _context.SaveChangesAsync();
            return cuestionario.IdCuestionario;
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

            public async Task<CuestionarioResponse> Update( CuestionarioDTO cuestionario)
            {
            if (cuestionario.IdCuestionario != 0)
            {
              var cuest=await  _context.Cuestionarios.FindAsync(cuestionario.IdCuestionario);
                if (cuest != null)
                {
                    cuest.Titulo = cuestionario.Titulo;

                    _context.Cuestionarios.Update(cuest);
                    await _context.SaveChangesAsync();
                    return new CuestionarioResponse(cuestionario.IdCuestionario,"",true);
                }
            }
            return new CuestionarioResponse(cuestionario.IdCuestionario, "Ha ocurrido un error ", false);

            }
        }
    }
