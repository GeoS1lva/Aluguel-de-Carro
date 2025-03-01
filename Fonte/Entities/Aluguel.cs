using System.ComponentModel.DataAnnotations.Schema;
using Fonte.Enums;
using Microsoft.Identity.Client;

namespace Fonte.Entities;

public sealed class Aluguel(int quantidadeDias, double valorTotal, DateOnly dataRetirada, DateOnly dataDevolucao, int carroId, int clienteId) : Entidade
{
    public int QuantidadeDias { get; set; } = quantidadeDias;
    public double ValorTotal { get; set; } = valorTotal;

    public DateOnly DataRetirada { get; set; } = dataRetirada;
    public DateOnly DataDevolucao { get; set; } = dataDevolucao;

    public int CarroId { get; set; } = carroId;
    public Carro Carro { get; set; }

    public int ClienteId { get; set; } = clienteId;
    public Cliente Cliente { get; set; }
}
