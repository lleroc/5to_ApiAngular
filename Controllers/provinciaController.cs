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
    public class provinciaController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public provinciaController(AngularDbContext context)
        {
            _context = context;
        }

        // GET: api/provincia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<provinciaModel>>> GetProvincias()
        {
            return await _context.Provincias.Include(pi => pi.Pais).ToListAsync();
        }

        // GET: api/provincia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<provinciaModel>> GetprovinciaModel(int id)
        {
            var provinciaModel = await _context.Provincias.Include(pi => pi.Pais)
                .FirstOrDefaultAsync(p => p.id == id);

            if (provinciaModel == null)
            {
                return NotFound();
            }

            return provinciaModel;
        }

        // PUT: api/provincia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutprovinciaModel(int id, provinciaModel provinciaModel)
        {
            if (id != provinciaModel.id)
            {
                return BadRequest();
            }

            _context.Entry(provinciaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!provinciaModelExists(id))
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

        // POST: api/provincia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<provinciaModel>> PostprovinciaModel(provinciaModel provinciaModel)
        {
            _context.Provincias.Add(provinciaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetprovinciaModel", new { id = provinciaModel.id }, provinciaModel);
        }

        // DELETE: api/provincia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteprovinciaModel(int id)
        {
            var provinciaModel = await _context.Provincias.FindAsync(id);
            if (provinciaModel == null)
            {
                return NotFound();
            }

            _context.Provincias.Remove(provinciaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool provinciaModelExists(int id)
        {
            return _context.Provincias.Any(e => e.id == id);
        }
    }
}
