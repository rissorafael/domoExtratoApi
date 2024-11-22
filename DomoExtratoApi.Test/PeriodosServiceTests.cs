using AutoMapper;
using DomoExtrato.Domain.Entities;
using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Models;
using DomoExtrato.Service.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DomoExtratoApi.Test
{
    public class PeriodosServiceTests
    {

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<PeriodosService>> _mockLogger;
        private readonly Mock<IPeriodosRepository> _mockPeriodosRepository;
        private readonly PeriodosService _periodosService;


        public PeriodosServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<PeriodosService>>();
            _mockPeriodosRepository = new Mock<IPeriodosRepository>();

            // Inicializando o serviço com as dependências mockadas
            _periodosService = new PeriodosService(_mockMapper.Object, _mockLogger.Object, _mockPeriodosRepository.Object);

        }
        [Fact]
        public async Task GetAllPeriodosAsync_ShouldReturnCorrectListOfPeriodosResponseModel()
        {
            // Arrange: Criar dados mockados para simular o retorno do repositório
            var periodosMock = new List<Periodos>
            {
            new Periodos { Id = 1, PeriodoDias = 30 },
            new Periodos { Id = 2, PeriodoDias = 60 }
        };

            var expectedResponse = new List<PeriodosResponseModel>
        {
            new PeriodosResponseModel { Id = 1, PeriodoDias = 30 },
            new PeriodosResponseModel { Id = 2, PeriodoDias = 60 }
        };

            // Configurando o comportamento dos mocks
            _mockPeriodosRepository.Setup(repo => repo.GetAllPeriodosAsync())
                .ReturnsAsync(periodosMock);  // O repositório vai retornar periodosMock

            _mockMapper.Setup(mapper => mapper.Map<List<PeriodosResponseModel>>(periodosMock))
                .Returns(expectedResponse);  // O mapeamento vai retornar expectedResponse

            // Act: Chamar o método que estamos testando
            var result = await _periodosService.GetAllPeriodosAsync();

            // Assert: Validar se o retorno é igual ao esperado
            Assert.NotNull(result);  // Verifica se o resultado não é nulo
            Assert.Equal(expectedResponse.Count, result.Count);  // Verifica se o número de elementos é igual

            // Verifica se os valores dos elementos retornados são os mesmos
            for (int i = 0; i < expectedResponse.Count; i++)
            {
                Assert.Equal(expectedResponse[i].Id, result[i].Id);
                Assert.Equal(expectedResponse[i].PeriodoDias, result[i].PeriodoDias);
            }
        }
    }
}