using System.Data;
using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Domain.FileManagement.ValueObjects;
using Dapper;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadataDeletedOutbox;

internal sealed class FileManagementOutboxRepository(
    IDbConnection dbConnection,
    IDbTransaction? dbTransaction = null) : IFileManagementOutboxRepository
{
    public async Task<FileManagementOutboxId> AddAsync(FileManagementOutbox fileManagementOutbox, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            insert into file_metadata_deleted_outbox (event_id, file_metadata_id, outbox_status, created_at)
            values (@EventId, @FileMetadataId, @OutboxStatus, @CreatedAt)
            returning event_id;
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            fileManagementOutbox,
            dbTransaction,
            cancellationToken: cancellationToken);
        var eventId = await dbConnection.QueryFirstAsync<FileManagementOutboxId>(commandDefinition);
        return eventId;
    }

    public async Task<IReadOnlyList<FileManagementOutbox>> GetTopUnprocessedOutboxes(
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
        var outboxes = await dbConnection.QueryAsync<FileManagementOutbox>(commandDefinition);
        return outboxes.ToList();
    }
}
