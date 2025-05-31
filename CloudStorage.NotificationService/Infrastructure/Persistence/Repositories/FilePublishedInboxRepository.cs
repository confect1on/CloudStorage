using CloudStorage.NotificationService.Infrastructure.Abstractions.Repositories;
using CloudStorage.NotificationService.Persistence;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace CloudStorage.NotificationService.Infrastructure.Persistence.Repositories;

internal sealed class FilePublishedInboxRepository(IOptions<PersistenceSettings> options) : IFilePublishedInboxRepository
{
    public async Task<FilePublishedInboxDto> GetFirstUnprocessedWithLeaseAsync(CancellationToken cancellationToken = default)
    {
        var connection = await GetConnectionAsync(cancellationToken);
        //language=sql
        const string sql =
            """
            with locked_rows as (
                     SELECT id
                       FROM file_published_event_inbox
                      WHERE processed_at IS NULL
                        AND processing_at IS NULL
                      ORDER BY created_at
                      LIMIT 1
                          FOR UPDATE SKIP LOCKED
                 )
               update file_published_event_inbox
                  set processing_at = @Now
                 from locked_rows
                where file_published_event_inbox.id = locked_rows.id
            returning *;               
            """;
        var commandDefinition = new CommandDefinition(
            sql,
            new
            {
                Now = DateTimeOffset.UtcNow,
            },
            cancellationToken: cancellationToken);
        var filePublishedInboxDto = await connection.QueryFirstAsync<FilePublishedInboxDto>(commandDefinition);
        return filePublishedInboxDto;
    }

    public async Task UpdateProcessedAt(Guid inboxId, CancellationToken cancellationToken = default)
    {
        var connection = await GetConnectionAsync(cancellationToken);
        //language=sql
        const string sql =
            """
            with locked_rows as (
                     SELECT id
                       FROM file_published_event_inbox
                      WHERE id = @InboxId
                      ORDER BY created_at
                      LIMIT 1
                          FOR UPDATE SKIP LOCKED
                 )
               update file_published_event_inbox
                  set processed_at = @Now
                 from locked_rows
                where file_published_event_inbox.id = locked_rows.id;               
            """;
        var commandDefinition = new CommandDefinition(
            sql,
            new
            {
                InboxId = inboxId,
                Now = DateTimeOffset.UtcNow,
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(commandDefinition);
    }

    private async Task<NpgsqlConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(options.Value.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}
