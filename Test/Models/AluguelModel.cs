using Test.Enums;

namespace Test.Models
{
    public class AluguelModel(string cpfCliente, string nomeCliente, string emailCliente, string cepCliente, int quantidadeDias, double valorTotal, string modeloCarro, string marcaCarro, int anoCarro, TipoCarro tipoCarro)
    {
        public string CpfCliente { get; set; } = cpfCliente;
        public string NomeCliente { get; set; } = nomeCliente;
        public string EmailCliente { get; set; } = emailCliente;
        public string CepCliente { get; set; } = cepCliente;
        public int QuantidadeDias { get; set; } = quantidadeDias;
        public double ValorTotal { get; set; } = valorTotal;
        public string ModeloCarro { get; private set; } = modeloCarro;
        public string MarcaCarro { get; private set; } = marcaCarro;
        public int AnoCarro { get; private set; } = anoCarro;
        public TipoCarro TipoCarro { get; private set; } = tipoCarro;


    }
}
