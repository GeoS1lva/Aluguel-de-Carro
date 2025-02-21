using Fonte.Enums;

namespace Fonte.Models;

public class SolicitacaoAluguelCarroModel
{
    public required string CpfCliente { get; set; }
    public required string NomeCliente { get; set; }
    public required string EmailCliente { get; set; }
    public required string CepCliente { get; set; }
    public DateOnly DataRetirada { get; set; }
    public DateOnly DataDevolucao { get; set; }
    public TipoCarro TipoCarro { get; set; }
}
