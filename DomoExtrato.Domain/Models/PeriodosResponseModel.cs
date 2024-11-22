using DomoExtrato.Domain.Validators;

namespace DomoExtrato.Domain.Models
{
    public class PeriodosResponseModel : Validator
    {
        public int Id { get; set; }
        public int PeriodoDias { get; set; }
    }
}
