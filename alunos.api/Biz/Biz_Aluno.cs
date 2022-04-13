using alunos.api.Context;
using alunos.api.Data;
using alunos.api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alunos.api.Biz
{
    public class Biz_Aluno
    {

        public readonly AppDbContext _context;
        public Biz_Aluno(AppDbContext context)
        {
            _context = context;
        }
        public static void InserirRegistros(List<string> nomes)
        {
            if (nomes.Count >= 5)
            {
                AlunoRepositorioImpl repo = new AlunoRepositorioImpl();
                repo.Inserir(nomes);
            }
        }

        public Aluno GetAluno(string nome)
        {
            AlunoRepositorioImpl data = new AlunoRepositorioImpl();
            return data.GetAluno(nome);
        }
    }
}
