using System.Data.Common;
using Dapper;
using MyFi.TheBadlands.Database;
using MyFi.TheBadlands.Models;

namespace MyFi.TheBadlands.Repositories;

public class QueryRepository
{
    private readonly IDbConnectionConfig _dbConnection;

    public QueryRepository(IDbConnectionConfig dbConnection)
    {
        _dbConnection = dbConnection;
    }

    private DbConnection GetConnection() => _dbConnection.GetReadConnection();

    public Task<IEnumerable<User>> GetUsers()
    {
        using var conn = GetConnection();
        return conn.QueryAsync<User>("SELECT id, name FROM User");
    }

    public Task<User> GetUser(Guid id)
    {
        using var conn = GetConnection();
        return conn.QueryFirstOrDefaultAsync<User>("SELECT id, name FROM User WHERE id = @id", new
        {
            id
        });
    }
}