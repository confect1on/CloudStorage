using Npgsql;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.ConnectionFactory;

public interface IConnectionFactory
{
    Task<NpgsqlConnection> CreateAsync();
    
    NpgsqlConnection Create();
}
