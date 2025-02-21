using Fonte.Enums;

namespace Fonte.Entities;
public class Carro(string modelo, string marca, int ano, TipoCarro tipo, int quantidadeDisponivel, double valorAluguelDia) : Entidade
{
    public string Modelo { get; private set; } = modelo;
    public string Marca { get; private set; } = marca;
    public int Ano { get; private set; } = ano;
    public TipoCarro Tipo { get; private set; } = tipo;
    public int QuantidadeDisponivel { get; private set; } = quantidadeDisponivel;
    public double ValorAluguelDia { get; private set; } = valorAluguelDia;

    public void AlugarUm()
        => QuantidadeDisponivel -= 1;

}
