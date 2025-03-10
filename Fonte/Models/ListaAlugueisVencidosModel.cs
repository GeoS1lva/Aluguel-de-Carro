namespace Fonte.Models
{
    public class ListaAlugueisVencidosModel
    {
        public int IdAluguel { get; set; }
        public int ClienteId { get; set; }
        public int CarroId { get; set; }
        public DateOnly DataDevolucaoPrevista { get; set; }

        public ListaAlugueisVencidosModel(int idAluguel, int clienteId, int carroId, DateOnly dataDevolucaoPrevista)
        {
            IdAluguel = idAluguel;
            ClienteId = clienteId;
            CarroId = carroId;
            DataDevolucaoPrevista = dataDevolucaoPrevista;
        }

    }
}
