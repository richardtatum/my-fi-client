using System.Data.Common;

namespace MyFi.TheBadlands.Database;
public interface IDbConnectionConfig
{
    public DbConnection GetConnection();
    public DbConnection GetReadConnection();
}