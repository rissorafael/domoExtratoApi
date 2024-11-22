using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomoExtratoApi.Controllers
{
    public class ExtratoController : ControllerBase
    {
        private readonly ILogger<ExtratoController> _logger;
        private readonly IPeriodosService _periodosService;
        private readonly IExtratoService _extratoService;

        public ExtratoController(ILogger<ExtratoController> logger, IPeriodosService periodosService, IExtratoService extratoService)
        {
            _logger = logger;
            _periodosService = periodosService;
            _extratoService = extratoService;
        }

        /// <summary>
        /// Consulta todos os períodos.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna todos os períodos</response>
        ///  <response code ="204">Nenhum período foi encontrado</response>
        [HttpGet("allperiodos")]
        [ProducesResponseType(typeof(PeriodosResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllPeriodosAsync()
        {
            var response = await _periodosService.GetAllPeriodosAsync();

            if (!response.First().IsValid)
                return StatusCode(response.First().StatusCode, response.First().Description);

            return Ok(response);

        }

        /// <summary>
        /// Consulta o extrato conforme o período.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna o extrato conforme o período</response>
        ///  <response code ="204">Nenhum extrato foi encontrado</response>
        [HttpGet("extrato/{periodoDias}")]
        [ProducesResponseType(typeof(ExtratoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetExtratoByPeriodoAsync(int periodoDias)
        {
            var response = await _extratoService.GetExtratoByPeriodoAsync(periodoDias);

            if (!response.First().IsValid)
                return StatusCode(response.First().StatusCode, response.First().Description);

            return Ok(response);

        }


        /// <summary>
        /// Gera pdf do extrato conforme o período.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna o pdf do extrato conforme o período</response>
        ///  <response code ="204">Nenhum extrato foi encontrado</response>
        [HttpGet("extratopdf/{periodoDias}")]
        [ProducesResponseType(typeof(ExtratoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GerarPdf(int periodoDias)
        {
            var response = await _extratoService.GerarPdfDeObjetoAsync(periodoDias);
           
            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return File(response.Pdf, "application/pdf", "extrato.pdf");
        }
    }
}
