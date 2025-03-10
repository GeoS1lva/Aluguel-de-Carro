namespace Fonte.Models
{
    public class ResultDevolucaoModel(double taxaAtraso, double valorFinal)
    {
        public double TaxaAtraso { get; set; } = taxaAtraso;
        public double ValorFinal { get; set; } = valorFinal;
    }
}
