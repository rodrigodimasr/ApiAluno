using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace alunos.api.Serives
{
    public class Worker : IHostedService
    {
        private Timer _timer;
        int temp = 1000;
        private Uri BaseAddress;

        public Task StartAsync(CancellationToken cancellationToken)
        {

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
    }
}
