using DomoExtrato.Domain.Validators;

namespace DomoExtrato.Domain.Models
{
    public class ExtratoResponseModel : Validator
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string TipoTransacao { get; set; }
        public decimal ValorMonetario { get; set; }
    }
}
