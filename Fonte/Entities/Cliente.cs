 namespace Fonte.Entities
{
    public sealed class Cliente(string nome, string cpf, string email, string cep) : Entidade
    {
        public string Nome { get; set; } = nome;
        public string Cpf { get; set; } = cpf;
        public string Email { get; set; } = email;
        public string Cep { get; set; } = cep;
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
