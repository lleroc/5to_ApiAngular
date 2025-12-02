using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAngular;
using ApiAngular.Models;

namespace ApiAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class direccionController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public direccionController(AngularDbContext context)
        {
            _context = context;
        }

        // GET: api/direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<direccionModel>>> GetDirecciones()
        {
            return await _context.Direcciones
                .Include(c => c.Canton)
                .Include(p => p.Canton.Provincia)
                .Include(pi=> pi.Canton.Provincia.Pais).ToListAsync();
        }

        // GET: api/direccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<direccionModel>> GetdireccionModel(int id)
        {
            var direccionModel = await _context.Direcciones.Include(c => c.Canton)
                .Include(p => p.Canton.Provincia)
                .Include(pi => pi.Canton.Provincia.Pais)
                .FirstOrDefaultAsync(d => d.id == id);

            if (direccionModel == null)
            {
                return NotFound();
            }

            return direccionModel;
        }

        // PUT: api/direccion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutdireccionModel(int id, direccionModel direccionModel)
        {
            if (id != direccionModel.id)
            {
                return BadRequest();
            }

            _context.Entry(direccionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!direccionModelExists(id))
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

        // POST: api/direccion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<direccionModel>> PostdireccionModel(direccionModel direccionModel)
        {
            _context.Direcciones.Add(direccionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetdireccionModel", new { id = direccionModel.id }, direccionModel);
        }

        // DELETE: api/direccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedireccionModel(int id)
        {
            var direccionModel = await _context.Direcciones.FindAsync(id);
            if (direccionModel == null)
            {
                return NotFound();
            }

            _context.Direcciones.Remove(direccionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool direccionModelExists(int id)
        {
            return _context.Direcciones.Any(e => e.id == id);
        }
    }
}
