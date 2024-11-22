using DomoExtrato.Domain.Interfaces;
using DomoExtrato.Infra.CrossCutting.ExtensionMethods;
using Microsoft.Data.SqlClient;


namespace DomoExtrato.Infra.Data.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        public SqlConnection Connection()
        {
            var connectionString = ValidaVariaveisDeConexaoBD();

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi possivel realizar a conexao com o banco de dados ");

            var connection = new SqlConnection(connectionString);
            return connection;
        }

        private string ValidaVariaveisDeConexaoBD()
        {
            var connectionString = ExatractConfiguration.GetConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // _logger.LogInformation("Não foi encontrado a variavel de ambiente de conexão com o banco de dados");
                return string.Empty;
            }

            return connectionString;
        }
    }
}
