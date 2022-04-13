using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using alunos.api.Context;
using alunos.api.Data;

namespace alunos.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporizadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TemporizadorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Temporizador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temporizador>>> GetTemporizador()
        {
            return await _context.Temporizador.ToListAsync();
        }


        // PUT: api/Temporizador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemporizador(int id, Temporizador temporizador)
        {
            if (id != temporizador.Id)
            {
                return BadRequest();
            }

            _context.Entry(temporizador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemporizadorExists(id))
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

        // POST: api/Temporizador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Temporizador>> PostTemporizador(Temporizador temporizador)
        //{
        //    _context.Temporizador.Add(temporizador);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTemporizador", new { id = temporizador.Id }, temporizador);
        //}

        //// DELETE: api/Temporizador/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTemporizador(int id)
        //{
        //    var temporizador = await _context.Temporizador.FindAsync(id);
        //    if (temporizador == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Temporizador.Remove(temporizador);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool TemporizadorExists(int id)
        {
            return _context.Temporizador.Any(e => e.Id == id);
        }
    }
}
