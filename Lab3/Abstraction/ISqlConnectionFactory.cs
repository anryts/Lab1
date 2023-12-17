using Microsoft.Data.Sqlite;

namespace Lab3.Abstraction;

public interface ISqlConnectionFactory
{
    /// <summary>
    /// Create new sql connection without bothering yourself
    /// about connection string
    /// </summary>
    /// <returns></returns>
    SqliteConnection CreateConnection();
}