using alunos.api.Context;
using alunos.api.Data;
using alunos.api.Interface;
using System.Collections.Generic;

namespace alunos.api.Repositorio
{
    public class AlunoRepositorioImpl : AlunoRepositorio
    {
        public readonly AppDbContext _context;
        public AlunoRepositorioImpl(AppDbContext context)
        {
            _context = context;

        }
        public void Inserir(List<Aluno> alunos)
        {
            if (alunos.Count > 0)
            {
                _context.Alunos.AddRange(alunos);
                _context.SaveChangesAsync();
            }
        }
    }
}
