 namespace Fonte.Entities
{
    public class Cliente(string nomeCliente, string cpfCliente, string emailCliente, string cepCliente)
    {
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = nomeCliente;
        public string CpfCliente { get; set; } = cpfCliente;
        public string EmailCliente { get; set; } = emailCliente;
        public string CepCliente { get; set; } = cepCliente;
    }
}
