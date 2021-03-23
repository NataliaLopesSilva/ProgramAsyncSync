using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace ProgramacaoSincrona
{
    class Program
    {
        /// <summary>
        /// Metódo principal
        /// </summary>
        /// <returns></returns>
        public static void Main()
        {
            try
            {
                //Pega o tempo inicial
                DateTime tempoInicial = DateTime.Now;

                //Dispara uma msg no console
                Console.WriteLine("--PROGRAMA SÍNCRONO");

                //Chama o método executar
                Executar();

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
        public static void Executar()
        {
            //Dispara uma msg no console
            Console.WriteLine("----Início Executar");

            HttpClient client = new();

            //* Injeção de dependência com o client nos métodos ObterDadosApi1Async e ObterDadosApi2Async
            //* Chamada dos métodos ObterDadosApi1Sync, ObterDadosApi2Sync e ObterDadosInternosSync
            var api1 = ObterDadosApi1Sync(client);
            var api2 = ObterDadosApi2Sync(client);
            var interno = ObterDadosInternosSync();

            //Obtenção dos resultados
            var dadosApi1 = api1;
            var dadosApi2 = api2;
            var dadosInterno = interno;

            //Dispara uma msg no console
            Console.WriteLine("----Terminei Executar");
        }

        /// <summary>
        /// Chamada api teste 1
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static List<Product> ObterDadosApi1Sync(HttpClient client)
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada api 1");

            //Chama a api e aguarda até que tenha o resultado
            var response = client.GetAsync("http://makeup-api.herokuapp.com/api/v1/products.json?brand=maybelline").GetAwaiter().GetResult();
            var api1Resultado = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            //Serealiza o resultado da api
            List<Product> lista = JsonConvert.DeserializeObject<List<Product>>(api1Resultado);

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada api 1");

            return lista;
        }

        /// <summary>
        /// Chamada api teste 2
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static List<Product> ObterDadosApi2Sync(HttpClient client)
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada api 2");

            //Chama a api e aguarda até que tenha o resultado
            var response = client.GetAsync("http://makeup-api.herokuapp.com/api/v1/products.json?brand=covergirl&product_type=lipstick").GetAwaiter().GetResult();
            var api2Resultado = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            //Serealiza o resultado da api
            List<Product> lista = JsonConvert.DeserializeObject<List<Product>>(api2Resultado);

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada api 2");

            return lista;
        }

        /// <summary>
        /// Simulação de uma busca de dados
        /// </summary>
        /// <returns></returns>
        private static List<Entrega> ObterDadosInternosSync()
        {
            //Dispara uma msg no console
            Console.WriteLine("------Chamada popula dados");

            //Chama o método
            var dadosInternoResultado = BuscarSync();

            //Dispara uma msg no console
            Console.WriteLine("------Finalizei chamada popula dados");

            return dadosInternoResultado;
        }

        /// <summary>
        /// Popula os dados + um intervalo de tempo de 3 segundos
        /// </summary>
        /// <returns></returns>
        private static List<Entrega> BuscarSync()
        {
            List<Entrega> lista = new();

            for (int i = 0; i < 100000; i++)
            {
                Entrega entrega = new("Caminhoneiro " + i, i, i);

                lista.Add(entrega);
            }

            Thread.Sleep(3000);

            return lista;
        }
    }
}
