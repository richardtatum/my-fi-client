using System.Data.Common;
using Dapper;
using MyFi.TheBadlands.Database;

namespace MyFi.TheBadlands.Repositories;

public class CommandRepository
{
    private readonly IDbConnectionConfig _dbConnection;

    public CommandRepository(IDbConnectionConfig dbConnection)
    {
        _dbConnection = dbConnection;
    }

    private DbConnection GetConnection() => _dbConnection.GetConnection();

    public async Task<Guid> InsertUser(string name)
    {
        var id = Guid.NewGuid();
        using var conn = GetConnection();
        await conn.ExecuteAsync("INSERT INTO User (id, name) VALUES (@id, @name)", new
        {
            id,
            name
        });

        return id;
    }

    public Task UpdateUser(Guid id, string name)
    {
        using var conn = GetConnection();
        return conn.ExecuteAsync("UPDATE User SET name = @name WHERE id = @id", new
        {
            id,
            name
        });
    }

    public Task DeleteUser(Guid id)
    {
        using var conn = GetConnection();
        return conn.ExecuteAsync("DELETE FROM User WHERE id = @id", new { id });
    }
}