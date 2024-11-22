
using DomoExtrato.Domain.Entities;

namespace DomoExtrato.Domain.Interfaces
{
    public interface IPeriodosRepository
    {
        Task<IEnumerable<Periodos>> GetAllPeriodosAsync();
    }
}
