using System.Globalization;

namespace Fonte.Models
{
    public class ClienteModel(string nome, string cpf, string email, string cep)
    {
        public string Nome { get; set; } = nome;
        public string Cpf { get; set; } = cpf;
        public string Email { get; set; } = email;
        public string Cep { get; set; } = cep;
    }
}
