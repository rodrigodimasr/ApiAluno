using alunos.api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alunos.api.IData
{
    public interface AlunoRepositorio
    {
        //void Inserir(List<Aluno> alunos);
        void Inserir(List<string> nomes);

        Aluno GetAluno(string nome);
    }
}
