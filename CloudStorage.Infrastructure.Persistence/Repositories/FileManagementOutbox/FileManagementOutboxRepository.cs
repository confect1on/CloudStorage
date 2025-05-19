using System.Data;
using System.Text.Json;
using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using Dapper;

namespace CloudStorage.Infrastructure.Persistence.Repositories.FileManagementOutbox;

internal sealed class FileManagementOutboxRepository(
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    IDbConnection dbConnection,
    IDbTransaction? dbTransaction = null) : IFileManagementOutboxRepository
{
    public async Task<FileManagementOutboxId> AddAsync(FileService.Domain.FileManagement.Entities.FileManagementOutbox fileManagementOutbox, CancellationToken cancellationToken = default)
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
        var outbox = new FileService.Domain.FileManagement.Entities.FileManagementOutbox()
        {
            Id = FileManagementOutboxId.New(),
            Type = type,
            Content = JsonSerializer.Serialize(message),
            CreatedAt = dateTimeOffsetProvider.GetUtcNow(),
        };
        return AddAsync(outbox, cancellationToken);
    }

    public async Task<IReadOnlyList<FileService.Domain.FileManagement.Entities.FileManagementOutbox>> GetTopUnprocessedAsync(
        GetTopUnprocessedOutboxesModel model,
        CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            select id, type, content, created_at, processed_at, error_message
            from file_management_outbox
            where processed_at is null -- pending
            order by created_at asc
            limit @MaxUnprocessedOutboxesCount
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            model,
            dbTransaction,
            cancellationToken: cancellationToken);
        var outboxes = await dbConnection.QueryAsync<FileService.Domain.FileManagement.Entities.FileManagementOutbox>(commandDefinition);
        return outboxes.ToList();
    }

    public Task SetErrorMessageAsync(SetErrorMessageModel model, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            update file_management_outbox
            set error_message = @ErrorMessage
            where id = @Id
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            new
            {
                Id = model.FileManagementOutboxId,
                model.ErrorMessage
            },
            dbTransaction,
            cancellationToken: cancellationToken);
        return dbConnection.ExecuteAsync(commandDefinition);
    }

    public Task MarkProcessedAsync(MarkProcessedModel model, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            update file_management_outbox
            set processed_at = @ProcessedAt,
                error_message = null
            where id = @Id
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery,
            new
            {
                Id = model.FileManagementOutboxId,
                ProcessedAt = dateTimeOffsetProvider.GetUtcNow()
            },
            dbTransaction,
            cancellationToken: cancellationToken);
        return dbConnection.ExecuteAsync(commandDefinition);
    }
}
