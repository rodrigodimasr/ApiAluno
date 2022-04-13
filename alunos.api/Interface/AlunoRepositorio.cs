using alunos.api.Data;
using System.Collections.Generic;

namespace alunos.api.Interface
{
    public interface AlunoRepositorio
    {
        void Inserir(List<Aluno> alunos);
    }
}
