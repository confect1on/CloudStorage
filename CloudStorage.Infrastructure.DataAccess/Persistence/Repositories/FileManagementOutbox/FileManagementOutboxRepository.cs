using System.Data;
using System.Text.Json;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Domain.FileManagement.ValueObjects;
using Dapper;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileManagementOutbox;

internal sealed class FileManagementOutboxRepository(
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    IDbConnection dbConnection,
    IDbTransaction? dbTransaction = null) : IFileManagementOutboxRepository
{
    public async Task<FileManagementOutboxId> AddAsync(Domain.FileManagement.Entities.FileManagementOutbox fileManagementOutbox, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            insert into file_management_outbox (id, type, content, created_at, processed_at, error_message)
            values (@Id, @Type, @Content, @CreatedAt, @ProcessedAt, @ErrorMessage)
            returning id;
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            fileManagementOutbox,
            dbTransaction,
            cancellationToken: cancellationToken);
        var eventId = await dbConnection.QueryFirstAsync<FileManagementOutboxId>(commandDefinition);
        return eventId;
    }

    public Task<FileManagementOutboxId> AddAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var type = typeof(TMessage).FullName ?? throw new InvalidOperationException($"Generic type of {nameof(message)} is not instantiated.");
        var outbox = new Domain.FileManagement.Entities.FileManagementOutbox(
            FileManagementOutboxId.New(),
            type,
            JsonSerializer.Serialize(message),
            dateTimeOffsetProvider.GetUtcNow(),
            null,
            null
            );
        return AddAsync(outbox, cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.FileManagement.Entities.FileManagementOutbox>> GetTopUnprocessedOutboxes(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            select id, type, content, created_at, processed_at, error_message
            from file_management_outbox
            where processed_at != null -- pending
            order by created_at
            limit @MaxUnprocessedOutboxesCount
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            model,
            dbTransaction,
            cancellationToken: cancellationToken);
        var outboxes = await dbConnection.QueryAsync<Domain.FileManagement.Entities.FileManagementOutbox>(commandDefinition);
        return outboxes.ToList();
    }
}
