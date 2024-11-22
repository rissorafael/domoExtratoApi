using DomoExtrato.Domain.Models;

namespace DomoExtrato.Domain.Interfaces
{
    public interface IExtratoService
    {
        Task<List<ExtratoResponseModel>> GetExtratoByPeriodoAsync(int periodoDias);
        Task<ExtratoPdfResponseModel> GerarPdfDeObjetoAsync(int periodoDias);
    }
}
