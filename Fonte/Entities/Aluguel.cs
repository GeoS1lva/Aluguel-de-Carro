using System.ComponentModel.DataAnnotations.Schema;
using Fonte.Enums;

namespace Fonte.Entities;

public sealed class Aluguel(int quantidadeDias, double valorTotal, string modeloCarro, string marcaCarro, int anoCarro, TipoCarro tipoCarro, int clienteId) : Entidade
{
    public int QuantidadeDias { get; set; } = quantidadeDias;
    public double ValorTotal { get; set; } = valorTotal;
    public string ModeloCarro { get; private set; } = modeloCarro;
    public string MarcaCarro { get; private set; } = marcaCarro;
    public int AnoCarro { get; private set; } = anoCarro;
    public TipoCarro TipoCarro { get; private set; } = tipoCarro;

    public int ClienteId { get; set; } = clienteId;
    public Cliente Cliente { get; set; }
}
