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
    public class cantonController : ControllerBase
    {
        private readonly AngularDbContext _context;

        public cantonController(AngularDbContext context)
        {
            _context = context;
        }

        // GET: api/canton
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cantonModel>>> GetCantones()
        {
            return await _context.Cantones
                .Include(p => p.Provincia)
                .Include(pi => pi.Provincia.Pais)
                .ToListAsync();
        }

        // GET: api/canton/5
        [HttpGet("{id}")]
        public async Task<ActionResult<cantonModel>> GetcantonModel(int id)
        {
            var cantonModel = await _context.Cantones
                .Include(p => p.Provincia)
                .Include(pi => pi.Provincia.Pais)
                .FirstOrDefaultAsync(c=> c.id == id);

            if (cantonModel == null)
            {
                return NotFound();
            }

            return cantonModel;
        }

        // PUT: api/canton/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutcantonModel(int id, cantonModel cantonModel)
        {
            if (id != cantonModel.id)
            {
                return BadRequest();
            }

            _context.Entry(cantonModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cantonModelExists(id))
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

        // POST: api/canton
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<cantonModel>> PostcantonModel(cantonModel cantonModel)
        {
            _context.Cantones.Add(cantonModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetcantonModel", new { id = cantonModel.id }, cantonModel);
        }

        // DELETE: api/canton/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletecantonModel(int id)
        {
            var cantonModel = await _context.Cantones.FindAsync(id);
            if (cantonModel == null)
            {
                return NotFound();
            }

            _context.Cantones.Remove(cantonModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool cantonModelExists(int id)
        {
            return _context.Cantones.Any(e => e.id == id);
        }
    }
}
