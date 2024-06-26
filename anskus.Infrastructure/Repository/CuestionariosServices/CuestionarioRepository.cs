﻿using anskus.Domain.Authentication;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> Add(Cuestionario cuestionario, string email)
        {
            var Userid = userManager.FindByEmailAsync(email);
            cuestionario.IdUsuario = Userid.Result!.Id;
            if (!string.IsNullOrEmpty(cuestionario.IdUsuario))
            {
                _context.Cuestionarios.Add(cuestionario);
                await _context.SaveChangesAsync();
            }

           
            return cuestionario.IdCuestionario;
            }

            public async Task<bool> Update( Cuestionario cuestionario)
            {
            if (cuestionario.IdCuestionario != 0)
            {
              var cuest=await  _context.Cuestionarios.FindAsync(cuestionario.IdCuestionario);
                if (cuest != null)
                {
                    cuest.Titulo = cuestionario.Titulo;

                    _context.Cuestionarios.Update(cuest);
                    await _context.SaveChangesAsync();
                  return true;
                }
            }
            return false;
            }
            public Task<List<Cuestionario>> GetbyUser(string email)
        {
            var user = userManager.FindByEmailAsync(email);
            if(user.Result != null)
            {
                var cuest = _context.Cuestionarios.Where(x => x.IdUsuario == user.Result.Id).ToListAsync();
                if (cuest != null)
                {
                    return cuest;
                }
            }
            return null;

        }

        public async Task<Cuestionario> GetbyId(int id)
        {
                var cuestionario = await _context.Cuestionarios
                    .Where(x => x.IdCuestionario == id)
                    .Include(o => o.Pregunta)
                    .ThenInclude(x => x.Respuesta)
                    .FirstOrDefaultAsync();
                if (cuestionario != null)
                    return cuestionario;           
            return null;
        }
    }
}
