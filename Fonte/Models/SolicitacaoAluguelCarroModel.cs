using Fonte.Enums;

namespace Fonte.Models;

public class SolicitacaoAluguelCarroModel
{
    public required string CpfCliente { get; set; }
    public DateOnly DataRetirada { get; set; }
    public DateOnly DataDevolucao { get; set; }
    public TipoCarro TipoCarro { get; set; }
}
