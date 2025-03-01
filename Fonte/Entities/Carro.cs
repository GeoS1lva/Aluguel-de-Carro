using System.Globalization;
using Fonte.Enums;

namespace Fonte.Entities;
public class Carro(string modelo, string marca, int ano, TipoCarro tipo, string placaCarro, double valorAluguelDia) : Entidade
{
    public string Modelo { get; private set; } = modelo;
    public string Marca { get; private set; } = marca;
    public int Ano { get; private set; } = ano;
    public TipoCarro Tipo { get; private set; } = tipo;
    public string PlacaCarro { get; private set; } = placaCarro;
    public bool Disponivel { get; private set; } = true;
    public double ValorAluguelDia { get; private set; } = valorAluguelDia;

    public ICollection<Aluguel> Alugueis { get; set; }

    public void AlugarUm()
        => Disponivel = false;

    public void DevolverCarro()
        => Disponivel = true;

}
