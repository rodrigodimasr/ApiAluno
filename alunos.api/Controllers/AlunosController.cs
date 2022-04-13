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
    public class AlunosController : ControllerBase
    {

        const string erroCaracteres = "Nome do aluno ultrapassa {0} caracteres.";
        const string notFound = "Aluno não encontrado.";
        const string studentNotFound = "O aluno {0} não foi encontrado para realizar a operação desejada!";

        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("{nome}")]
        public async Task<ActionResult<Aluno>> GetAluno(string nome)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Nome == nome);

            if (aluno == null)
            {
                return NotFound(notFound);
            }

            return aluno;
        }

        // PUT: api/Aluno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest("Id divergente!");
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NotFound(notFound);
                }
            }

            return NoContent();
        }

        // POST: api/Aluno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            string mensagem = string.Empty;

            //realiza tratamento dos dados.
            Tratamento(aluno, out mensagem);

            if (mensagem != string.Empty)
                return BadRequest(mensagem);

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = aluno.Id }, aluno);
        }

        // DELETE: api/Aluno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound(String.Format(studentNotFound, id));
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }

        private void Tratamento(Aluno aluno, out string errorMensage)
        {
            errorMensage = string.Empty;

            #region tratamento qtde caracteres para cada campo. 

            if (aluno.Nome.Length > 50)
            {
                errorMensage = String.Format(erroCaracteres, 50);
            }

            if (aluno.SobreNome.Length > 30)
            {
                errorMensage = String.Format(erroCaracteres, 30);
            }

            if (aluno.Email.Length > 100)
            {
                errorMensage = String.Format(erroCaracteres, 100);
            }

            if (aluno.Telefone.Length > 15)
            {
                errorMensage = String.Format(erroCaracteres, 15);
            }

            #endregion
        }
    }
}
