using AutoMapper;
using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace DomoExtrato.Service.Service
{
    public class PeriodosService : IPeriodosService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PeriodosService> _logger;
        private readonly IPeriodosRepository _periodosRepository;

        public PeriodosService(IMapper mapper, ILogger<PeriodosService> logger, IPeriodosRepository periodosRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _periodosRepository = periodosRepository;
        }

        public async Task<List<PeriodosResponseModel>> GetAllPeriodosAsync()
        {
            var response = new List<PeriodosResponseModel>();

            try
            {
                var periodos = await _periodosRepository.GetAllPeriodosAsync();
                if (periodos == null)
                {
                    var error = new PeriodosResponseModel();
                    error.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe extrato dentro desse período");
                    response.Add(error);
                    return response;
                }

                response = _mapper.Map<List<PeriodosResponseModel>>(periodos);
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[PeriodosService - GetAllPeriodosAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }
    }
}
