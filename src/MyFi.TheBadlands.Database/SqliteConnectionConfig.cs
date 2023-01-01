using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace MyFi.TheBadlands.Database;

public class SqliteConnectionConfig : IDbConnectionConfig
{
    public SqliteConnectionConfig(string databaseName)
    {
        DatabaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
    }

    private string? DatabaseName { get; set; }

    public DbConnection GetConnection() => new SqliteConnection(DatabaseName);

    public DbConnection GetReadConnection() => new SqliteConnection(DatabaseName);
}
