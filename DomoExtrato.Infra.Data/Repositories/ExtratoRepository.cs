using DomoExtrato.Domain.Entities;
using DomoExtrato.Domain.Interfaces;
using Dapper;


namespace DomoExtrato.Infra.Data.Repositories
{
    public class ExtratoRepository : IExtratoRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ExtratoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Extrato>> GetExtratoByPeriodoAsync(int periodoDias)
        {

            using var connection = _connectionFactory.Connection();
            {
                string query = $@"SELECT 
                                      Id,
                                      Data,
                                      TipoTransacao,
                                      ValorMonetario
                                    FROM Extrato
                                       WHERE Data BETWEEN DATEADD(DAY, -@periodoDias, GETDATE()) AND GETDATE();";

                var result = await connection.QueryAsync<Extrato>(query, new
                {
                    periodoDias = periodoDias
                });

                return result;
            }
        }
    }
}

