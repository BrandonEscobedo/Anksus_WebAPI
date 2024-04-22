 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using anskus.Application.DTOs.Cuestionarios;
using anskus.Domain.Models.dbModels;
using anskus.Infrastructure.Data;

namespace Anksus_WebAPI.Server.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly TestAnskusContext _context;

        public CategoriasController(TestAnskusContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<Categoria>> GetCategorias()
        {
           
            {
                var responseAPI = new ResponseAPI<List<CategoriasDTO>>();
                var Categorias = new List<CategoriasDTO>();
                try
                {
                    foreach (var item in await _context.Categorias.ToListAsync())
                    {
                        Categorias.Add(new CategoriasDTO
                        {
                            IdCategoria = item.IdCategoria,
                            Categoria = item.Categoria1
                        });

                    }
                    responseAPI.EsCorrecto = true;
                    responseAPI.Valor = Categorias;
                }
                catch (Exception ex)

                {
                    responseAPI.EsCorrecto = false;
                    responseAPI.mensaje = "Ha ocurrido un error de tipo: " + ex.Message;
                }
                return Ok(responseAPI);
            }
        }

    }
}
