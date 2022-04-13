using alunos.api.Biz;
using alunos.api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace alunos.api.Serives
{
    public class Worker : IHostedService
    {

        private readonly AppDbContext _context;

        private Timer _timer;
        int temp = 5000;
        private Uri BaseAddress;

        public Task StartAsync(CancellationToken cancellationToken)
        {

            CalculateTimer(out temp);
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(temp));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            //configurando a chamada passndo o endereço da api.
            HttpClient client = new HttpClient { BaseAddress = new Uri("http://gerador-nomes.herokuapp.com/nomes/10") };

            //realizando a chamada da api e obtendo retorno.
            var response = await client.GetAsync(BaseAddress);

            //obtendo o o json com o conteudo retornado.
            var content = await response.Content.ReadAsStringAsync();

            //convertendo retorno json em uma lista de strings.
            var listaNomes = JsonConvert.DeserializeObject<List<string>>(content);

            //chamada do metodo que instacia o objeto aluno e realiza operação de inserção.
            Biz.Biz_Aluno.InserirRegistros(listaNomes);

        }

        private void CalculateTimer(out int tempo)
        {
            tempo = 5;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=TI-1266\\SQLEXPRESS;Database=Cadastro;Trusted_connection=True;MultipleActiveResultSets=true");
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                //busca o valor do tempo que a aplicação deve aguardar para inserir novos registros
                var temporizador = context.Temporizador.AsEnumerable().Select(x => x).ToList();
                if (temporizador != null)
                {
                    //como só tem um registro deixei fixo, e a api não tem opção de post, apenas via banco de dados.
                    tempo = temporizador[0].Temp;
                }
            }

        }
    }
}
