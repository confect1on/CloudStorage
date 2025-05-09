using System.Transactions;
using Npgsql;

namespace CloudStorage.Infrastructure.DataAccess;

public class PostgresRepository
{
    private readonly DalSettings _dalSettings;

    protected const int DefaultTimeoutInSeconds = 5;

    protected PostgresRepository(DalSettings dalSettings)
    {
        _dalSettings = dalSettings;
    }

    protected async Task<NpgsqlConnection> GetConnectionAsync()
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new InvalidOperationException("Transaction is aborted.");
        }
        var connection = new NpgsqlConnection(_dalSettings.PostgresConnectionString);
        await connection.OpenAsync();
        await connection.ReloadTypesAsync();
        return connection;
    }
}
