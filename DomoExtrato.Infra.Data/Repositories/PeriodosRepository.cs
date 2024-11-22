using Dapper;
using DomoExtrato.Domain.Entities;
using DomoExtrato.Domain.Interfaces;


namespace DomoExtrato.Infra.Data.Repositories
{
    public class PeriodosRepository : IPeriodosRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PeriodosRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Periodos>> GetAllPeriodosAsync()
        {

            using var connection = _connectionFactory.Connection();
            {
                var result = await connection.QueryAsync<Periodos>(@"
                                                    SELECT 
                                                       Id
                                                      ,PeriodoDias
                                                    FROM Periodos
                                                      Where 1=1");
                return result;
            }
        }
    }
}
