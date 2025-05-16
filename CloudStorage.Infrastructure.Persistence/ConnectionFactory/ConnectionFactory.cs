using Microsoft.Extensions.Options;
using Npgsql;

namespace CloudStorage.Infrastructure.Persistence.ConnectionFactory;

internal sealed class ConnectionFactory(IOptions<DalSettings> options) : IConnectionFactory
{
    public async Task<NpgsqlConnection> CreateAsync()
    {
        var connection = new NpgsqlConnection(options.Value.PostgresConnectionString);
        await connection.OpenAsync();
        await connection.ReloadTypesAsync();
        return connection;
    }

    public NpgsqlConnection Create()
    {
        var connection = new NpgsqlConnection(options.Value.PostgresConnectionString);
        connection.Open();
        connection.ReloadTypes();
        return connection;
    }
}
