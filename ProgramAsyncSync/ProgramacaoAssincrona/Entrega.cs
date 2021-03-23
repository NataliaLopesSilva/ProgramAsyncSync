namespace ProgramacaoAssincrona
{
    public class Entrega
    {
        public string Caminhoneiro { get; set; }

        public int QtdPedido { get; set; }

        public int QtdPontoEntrega { get; set; }


        public Entrega(string caminhoneiro, int qtdPedido, int qtdPontoEntrega)
        {
            Caminhoneiro = caminhoneiro;
            QtdPedido = qtdPedido;
            QtdPontoEntrega = qtdPontoEntrega;
        }
    }
}
