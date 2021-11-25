using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projur.BackEnd.Projur.Domain;
using Projur.BackEnd.Projur.Infrastructure.Database;
using Projur.Models;

namespace Projur.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetPagamentos()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuarios = await _context.Pagamentos.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.Id)
            {
                return BadRequest();
            }

            #region Validações
            if (!string.IsNullOrEmpty(usuarios?.Email) && !Validador.IsValidEmail(usuarios.Email))
                return BadRequest("E-mail Inválido");
            if (usuarios?.DataNascimento != null && Validador.IsDateBiggerThanToday((System.DateTime)usuarios.DataNascimento))
                return BadRequest("Data de Nascimento maior que Hoje");
            #endregion

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            #region Validações
            if (!string.IsNullOrEmpty(usuarios?.Email) && !Validador.IsValidEmail(usuarios.Email))
                return BadRequest("E-mail Inválido");
            if (usuarios?.DataNascimento != null && Validador.IsDateBiggerThanToday((System.DateTime)usuarios.DataNascimento))
                return BadRequest("Data de Nascimento maior que Hoje");
            #endregion

            _context.Pagamentos.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.Id }, usuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            var usuarios = await _context.Pagamentos.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(int id)
        {
            return _context.Pagamentos.Any(e => e.Id == id);
        }
    }
}
