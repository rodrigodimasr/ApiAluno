using alunos.api.Context;
using alunos.api.Data;
using alunos.api.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                // Biz_Aluno teste = new Biz_Aluno();
                List<Aluno> listAlunos = new List<Aluno>();
                for (var i = 0; i <= 5; i++)
                {
                    Aluno aluno = new Aluno();
                    aluno.Nome = nomes[i].Trim();
                    aluno.Email = "teste@gmail.com";
                    aluno.Telefone = "123456789";

                    listAlunos.Add(aluno);
                }

                //Inserir(listAlunos);
            }
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
