
using DomoExtrato.Domain.Models;

namespace DomoExtrato.Domain.Interfaces
{
    public interface IPeriodosService
    {
        Task<List<PeriodosResponseModel>> GetAllPeriodosAsync();
    }
}
