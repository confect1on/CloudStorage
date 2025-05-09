using System.Transactions;
using Npgsql;

namespace CloudStorage.Infrastructure.DataAccess;

public class PostgresRepository
{
    private readonly DalOptions _dalOptions;

    protected const int DefaultTimeoutInSeconds = 5;

    protected PostgresRepository(DalOptions dalOptions)
    {
        _dalOptions = dalOptions;
    }

    protected async Task<NpgsqlConnection> GetConnectionAsync()
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new InvalidOperationException("Transaction is aborted.");
        }
        var connection = new NpgsqlConnection(_dalOptions.PostgresConnectionString);
        await connection.OpenAsync();
        await connection.ReloadTypesAsync();
        return connection;
    }
}
