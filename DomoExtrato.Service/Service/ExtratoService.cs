using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;


namespace DomoExtrato.Service.Service
{
    public class ExtratoService : IExtratoService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExtratoService> _logger;
        private readonly IExtratoRepository _extratoRepository;
        private readonly IConverter _converter;

        public ExtratoService(IMapper mapper, ILogger<ExtratoService> logger, IExtratoRepository extratoRepository, IConverter converter)
        {
            _mapper = mapper;
            _logger = logger;
            _extratoRepository = extratoRepository;
            _converter = converter;
        }

        public async Task<List<ExtratoResponseModel>> GetExtratoByPeriodoAsync(int periodoDias)
        {
            var response = new List<ExtratoResponseModel>();

            try
            {
                var extrato = await _extratoRepository.GetExtratoByPeriodoAsync(periodoDias);
                if (extrato == null)
                {
                    var error = new ExtratoResponseModel();
                    error.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe extrato dentro desse período");
                    response.Add(error);
                    return response;
                }

                response = _mapper.Map<List<ExtratoResponseModel>>(extrato);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ExtratoService - GetExtratoByPeriodoAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }


        public async Task<ExtratoPdfResponseModel> GerarPdfDeObjetoAsync(int periodoDias)
        {
            var response = new ExtratoPdfResponseModel();

            var extratos = await GetExtratoByPeriodoAsync(periodoDias);
            if (extratos == null)
            {
                response.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe extrato dentro desse período");
                return response;
            }

            var html = GerarHtmlDoRelatorio(extratos);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color, Orientation = Orientation.Portrait, PaperSize = PaperKind.A4 },
                Objects = { new ObjectSettings() { HtmlContent = html, WebSettings = { DefaultEncoding = "utf-8" } } }
            };

            var bytePdf = _converter.Convert(doc);
            response.Pdf = bytePdf;

            return response;
        }

        private string GerarHtmlDoRelatorio(List<ExtratoResponseModel> relatorio)
        {
            var sb = new StringBuilder();


            sb.AppendLine("<html><body>");
            sb.AppendLine("<h1>Relatorio Extratos</h1>");
            sb.AppendLine("<table border='1'>");


            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Data</th>");
            sb.AppendLine("<th>Valor Monetario</th>");
            sb.AppendLine("<th>Tipo Transacao</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");


            sb.AppendLine("<tbody>");
            foreach (var item in relatorio)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{item.Data}</td>");
                sb.AppendLine($"<td>{item.ValorMonetario:C}</td>");
                sb.AppendLine($"<td>{item.TipoTransacao}</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");


            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}
