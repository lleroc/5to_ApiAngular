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
    public class paisController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public paisController(AngularDbContext context)
        {
            _context = context;
        }

        // GET: api/pais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<paisModel>>> GetPaises()
        {
            return await _context.Paises.ToListAsync();
        }

        // GET: api/pais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<paisModel>> GetpaisModel(int id)
        {
            var paisModel = await _context.Paises.FindAsync(id);

            if (paisModel == null)
            {
                return NotFound();
            }

            return paisModel;
        }

        // PUT: api/pais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpaisModel(int id, paisModel paisModel)
        {
            if (id != paisModel.id)
            {
                return BadRequest();
            }

            _context.Entry(paisModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paisModelExists(id))
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

        // POST: api/pais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<paisModel>> PostpaisModel(paisModel paisModel)
        {
            _context.Paises.Add(paisModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetpaisModel", new { id = paisModel.id }, paisModel);
        }

        // DELETE: api/pais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepaisModel(int id)
        {
            var paisModel = await _context.Paises.FindAsync(id);
            if (paisModel == null)
            {
                return NotFound();
            }

            _context.Paises.Remove(paisModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool paisModelExists(int id)
        {
            return _context.Paises.Any(e => e.id == id);
        }
    }
}
