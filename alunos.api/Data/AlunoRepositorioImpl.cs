using alunos.api.Context;
using alunos.api.Entities;
using alunos.api.IData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alunos.api.Data
{
    public partial class AlunoRepositorioImpl : AlunoRepositorio
    {
        public readonly AppDbContext _context;
        const string connection = "Server=TI-1266\\SQLEXPRESS;Database=Cadastro;Trusted_connection=True;MultipleActiveResultSets=true";

        public AlunoRepositorioImpl()
        {
        }
        public AlunoRepositorioImpl(AppDbContext context)
        {
            _context = context;

        }
        public void Inserir(List<string> nomes)
        {
            if (nomes.Count > 0)
            {

                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

                optionsBuilder.UseSqlServer(connection);
                using (var context = new AppDbContext(optionsBuilder.Options))
                {
                    //seu código
                    List<Aluno> listAlunos = new List<Aluno>();
                    for (var i = 0; i < 5; i++)
                    {
                        Aluno aluno = new Aluno();
                        aluno.Nome = nomes[i].Trim();
                        aluno.Email = "teste@gmail.com";
                        aluno.Telefone = "123456789";

                        context.Alunos.Add(aluno);
                    }

                    context.SaveChanges();
                }
            }

        }

        public Aluno GetAluno(string nome)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(connection);
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                var alunos = context.Alunos.AsEnumerable().Select(x => x).ToList();

                return alunos.FirstOrDefault(x => x.Nome == nome);
            }
        }
    }
}
