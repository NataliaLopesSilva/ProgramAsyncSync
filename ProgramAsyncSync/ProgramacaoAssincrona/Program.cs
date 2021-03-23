using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgramacaoAssincrona
{
    public class Program
    {
        /// <summary>
        /// Metódo principal async
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            try
            {
                //Pega o tempo inicial
                DateTime tempoInicial = DateTime.Now;

                //Dispara uma msg no console
                Console.WriteLine("--PROGRAMA ASSÍNCRONO");

                //Chama o método executar, sinalizando a espera
                await Executar();

                //Pega o tempo final
                DateTime tempoFinal = DateTime.Now;

                //Calcula o tempo total da operação
                double tempoTotal = (tempoFinal - tempoInicial).TotalMilliseconds;

                //Dispara uma msg no console
                Console.WriteLine("Executado em: " + tempoTotal + " milisegundos");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Execução das operações
        /// </summary>
        /// <returns></returns>
        public static async Task Executar()
        {
            //Dispara uma msg no console
            Console.WriteLine("----Início Executar");

            HttpClient client = new();

            //* Injeção de dependência com o client nos métodos ObterDadosApi1Async e ObterDadosApi2Async
            //* Chamada dos métodos ObterDadosApi1Async, ObterDadosApi2Async e ObterDadosInternosAsync para que as
            //  tarefas sejam agendandas e colocadas na ThreadPool, ele chama um a um, para que eles sejam executados
            //  de maneira independente
            var api1Task = ObterDadosApi1Async(client);
            var api2Task = ObterDadosApi2Async(client);
            var internoTask = ObterDadosInternosAsync();

            //Obtenção dos resultados das tarefas, sinalizando que deve haver uma espera para pegar seus resultados
            //Quando os resultados estiverem prontos, aí sim retorna para as variáveis
            var dadosApi1 = await api1Task;
            var dadosApi2 = await api2Task;
            var dadosInterno = await internoTask;

            //Dispara uma msg no console
            Console.WriteLine("----Terminei Executar");
        }

        /// <summary>
        /// Chamada async api teste 1
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task<List<Product>> ObterDadosApi1Async(HttpClient client)
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada api 1");

            //Chama a api e aguarda até que tenha o resultado
            var api1Resultado = await client.GetStringAsync("http://makeup-api.herokuapp.com/api/v1/products.json?brand=maybelline");

            //Serealiza o resultado da api
            List<Product> lista = JsonConvert.DeserializeObject<List<Product>>(api1Resultado);

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada api 1");

            return lista;
        }

        /// <summary>
        /// Chamada async api teste 2
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task<List<Product>> ObterDadosApi2Async(HttpClient client)
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada api 2");

            //Chama a api e aguarda até que tenha o resultado
            var api2Resultado = await client.GetStringAsync("http://makeup-api.herokuapp.com/api/v1/products.json?brand=covergirl&product_type=lipstick");

            //Serealiza o resultado da api
            List<Product> lista = JsonConvert.DeserializeObject<List<Product>>(api2Resultado);

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada api 2");

            return lista;
        }

        /// <summary>
        /// Simulação async de uma busca de dados
        /// </summary>
        /// <returns></returns>
        private static async Task<List<Entrega>> ObterDadosInternosAsync()
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada popula dados");

            //Chama o método e sinaliza para aguardar seu retorno
            var dadosInternoResultado = await BuscarAsync();

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada popula dados");

            return dadosInternoResultado;
        }

        /// <summary>
        /// Popula os dados + um intervalo de tempo de 3 segundos
        /// </summary>
        /// <returns></returns>
        private static async Task<List<Entrega>> BuscarAsync()
        {
            List<Entrega> lista = new();

            for (int i = 0; i < 100000; i++)
            {
                Entrega entrega = new("Caminhoneiro " + i, i, i);

                lista.Add(entrega);
            }

            await Task.Delay(3000);

            return lista;
        }
    }
}
