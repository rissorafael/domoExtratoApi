using AutoMapper;
using DinkToPdf.Contracts;
using DomoExtrato.Domain.Entities;
using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Domain.Models;
using DomoExtrato.Service.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace DomoExtratoApi.Test
{
    public class ExtratoServiceTests
    {

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ExtratoService>> _mockLogger;
        private readonly Mock<IExtratoRepository> _mockExtratoRepository;
        private readonly Mock<IConverter> _mockConverter;
    

        private readonly ExtratoService _extratoService;

        public ExtratoServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ExtratoService>>();
            _mockExtratoRepository = new Mock<IExtratoRepository>();
            _mockConverter = new Mock<IConverter>();

            // Inicializando o serviço a ser testado
            _extratoService = new ExtratoService(_mockMapper.Object, _mockLogger.Object, _mockExtratoRepository.Object, _mockConverter.Object);

        }

        [Fact]
        public async Task GetExtratoByPeriodoAsync_ShouldReturnCorrectListOfExtratoResponseModel()
        {
            // Arrange: Criar dados de entrada e esperado
            var periodoDias = 30;

            var extratoMock = new List<Extrato>
        {
            new Extrato { Id = 1, Data = DateTime.Now, TipoTransacao = "Crédito", ValorMonetario = 100 },
            new Extrato { Id = 2, Data = DateTime.Now.AddDays(-1), TipoTransacao = "Débito", ValorMonetario = 50 }
        };

            var expectedResponse = new List<ExtratoResponseModel>
        {
            new ExtratoResponseModel { Id = 1, Data = DateTime.Now, TipoTransacao = "Crédito", ValorMonetario = 100 },
            new ExtratoResponseModel { Id = 2, Data = DateTime.Now.AddDays(-1), TipoTransacao = "Débito", ValorMonetario = 50 }
        };

            // Mockar o comportamento do repositório e do mapper
            _mockExtratoRepository.Setup(repo => repo.GetExtratoByPeriodoAsync(periodoDias))
                .ReturnsAsync(extratoMock);

            _mockMapper.Setup(mapper => mapper.Map<List<ExtratoResponseModel>>(extratoMock))
                .Returns(expectedResponse);

            // Act: Chamar o método a ser testado
            var result = await _extratoService.GetExtratoByPeriodoAsync(periodoDias);

            // Assert: Validar se o retorno é igual ao esperado
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count);

            for (int i = 0; i < expectedResponse.Count; i++)
            {
                Assert.Equal(expectedResponse[i].Id, result[i].Id);
                Assert.Equal(expectedResponse[i].Data, result[i].Data);
                Assert.Equal(expectedResponse[i].TipoTransacao, result[i].TipoTransacao);
                Assert.Equal(expectedResponse[i].ValorMonetario, result[i].ValorMonetario);
            }
        }
    }
}