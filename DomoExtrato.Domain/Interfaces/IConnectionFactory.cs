using Microsoft.Data.SqlClient;

namespace DomoExtrato.Domain.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection Connection();
    }
}
