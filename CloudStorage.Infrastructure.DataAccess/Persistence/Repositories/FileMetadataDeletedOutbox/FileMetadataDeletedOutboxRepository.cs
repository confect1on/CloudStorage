using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;
using Dapper;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadataDeletedOutbox;

internal sealed class FileMetadataDeletedOutboxRepository(
    IDbConnection dbConnection,
    IDbTransaction? dbTransaction = null) : IFileMetadataDeletedOutboxRepository
{
    public async Task<EventId> AddAsync(FileMetadataOutbox fileMetadataOutbox, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            insert into file_metadata_deleted_outbox (event_id, file_metadata_id, outbox_status, created_at)
            values (@EventId, @FileMetadataId, @OutboxStatus, @CreatedAt)
            returning event_id;
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            fileMetadataOutbox,
            dbTransaction,
            cancellationToken: cancellationToken);
        var eventId = await dbConnection.QueryFirstAsync<EventId>(commandDefinition);
        return eventId;
    }

    public async Task<IReadOnlyList<FileMetadataOutbox>> GetTopUnprocessedOutboxes(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            select event_id, file_metadata_id, outbox_status, created_at
            from file_metadata_deleted_outbox
            where outbox_status = 0 -- pending
            order by created_at
            limit @MaxUnprocessedOutboxesCount
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            model,
            dbTransaction,
            cancellationToken: cancellationToken);
        var outboxes = await dbConnection.QueryAsync<FileMetadataOutbox>(commandDefinition);
        return outboxes.ToList();
    }
}
