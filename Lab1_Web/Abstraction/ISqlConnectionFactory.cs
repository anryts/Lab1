using Microsoft.Data.SqlClient;

namespace Lab1_Web.Abstraction;

public interface ISqlConnectionFactory
{
    /// <summary>
    /// Create new sql connection without bothering yourself
    /// about connection string
    /// </summary>
    /// <returns></returns>
    SqlConnection CreateConnection();
}