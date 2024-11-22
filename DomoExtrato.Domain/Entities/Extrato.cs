

namespace DomoExtrato.Domain.Entities
{
    public class Extrato
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string TipoTransacao { get; set; }
        public decimal ValorMonetario { get; set; }
    }
}
