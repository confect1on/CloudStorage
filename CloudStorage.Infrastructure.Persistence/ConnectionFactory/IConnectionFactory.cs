using Npgsql;

namespace CloudStorage.Infrastructure.Persistence.ConnectionFactory;

public interface IConnectionFactory
{
    Task<NpgsqlConnection> CreateAsync();
    
    NpgsqlConnection Create();
}
