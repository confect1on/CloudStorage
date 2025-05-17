using AVScannerService.Persistence.Abstractions;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AVScannerService.Persistence;

internal sealed class FileCreatedEventInboxRepository(IOptions<PersistenceSettings> options) : IFileCreatedEventInboxRepository
{
    public async Task InsertAsync(FileCreatedEventDto fileCreatedEventDto, CancellationToken cancellationToken)
    {
        const string sql =
            """
            insert into file_created_event_inbox (id, aggregate_id, created_at, temporary_storage_id)
            values (@Id, @AggregateId, @CreatedAt, @TemporaryStorageId)
            on conflict on constraint "PK_file_created_event_inbox" do nothing;
            """;
        await using var connection = await GetConnectionAsync(cancellationToken);
        var command = new CommandDefinition(
            sql,
            fileCreatedEventDto,
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
    }

    private async Task<NpgsqlConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        var npgsqlConnection = new NpgsqlConnection(options.Value.PostgresConnectionString);
        await npgsqlConnection.OpenAsync(cancellationToken);
        return npgsqlConnection;
    }
}
