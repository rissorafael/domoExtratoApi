using DomoExtrato.Domain.Entities;

namespace DomoExtrato.Domain.Interfaces
{
    public interface IExtratoRepository
    {
        Task<IEnumerable<Extrato>> GetExtratoByPeriodoAsync(int periodoDias);
    }
}
