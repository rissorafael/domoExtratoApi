using DomoExtrato.Domain.Validators;

namespace DomoExtrato.Domain.Models
{
    public class ExtratoPdfResponseModel : Validator
    {
        public byte[] Pdf { get; set; }
    }
}
