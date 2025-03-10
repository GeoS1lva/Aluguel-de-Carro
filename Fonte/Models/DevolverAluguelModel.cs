namespace Fonte.Models
{
    public class DevolverAluguelModel
    {
        public required string Cpf { get; set; }
        public required string PlacaCarro { get; set; }

        public DateOnly DataDevolucaoDefinitiva { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
