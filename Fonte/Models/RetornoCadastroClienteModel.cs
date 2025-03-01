using System.Globalization;

namespace Fonte.Models
{
    public class RetornoCadastroClienteModel(string cpf)
    {
        public string cpf { get; set; } = cpf;
    }
}
