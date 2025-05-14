using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities.Ids;
using CloudStorage.Domain.Exceptions;
using Dapper;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadata;

internal sealed class FileMetadataRepository(
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    IDbConnection dbConnection,
    IDbTransaction? dbTransaction = null) : IFileMetadataRepository
{
    public async Task<FileMetadataId> AddAsync(Domain.Entities.FileMetadata fileMetadata, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            insert into file_metadata (id, user_id, storage_id, file_name, file_size_in_bytes, mime_type, created_at)
            values (@Id, @UserId, @StorageId, @FileName, @FileSizeInBytes, @MimeType, @CreatedAt)
            returning id;
            """;
        var commandDefinition = new CommandDefinition(sqlQuery, fileMetadata, dbTransaction, cancellationToken: cancellationToken);
        var fileMetaDataId = await dbConnection.QueryFirstAsync<FileMetadataId>(commandDefinition);
        return fileMetaDataId;
    }

    public async Task<Domain.Entities.FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            select id, user_id, storage_id, file_name, file_size_in_bytes, mime_type, created_at
            from file_metadata
            where id = @Id;
            """;
        var commandDefinition = new CommandDefinition(
            sqlQuery, new
            {
                Id = fileMetadataId
            },
            dbTransaction,
            cancellationToken: cancellationToken);
        var fileMetadata = await dbConnection.QueryFirstOrDefaultAsync<Domain.Entities.FileMetadata>(commandDefinition);
        if (fileMetadata is null)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
        return fileMetadata;
    }

    public async Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId? storageId, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            update file_metadata
            set storage_id = @StorageId
            where id = @Id;
            """;
        var command = new CommandDefinition(
            sqlQuery,
            new
            {
                Id = fileMetadataId,
                StorageId = storageId
            },
            dbTransaction,
            cancellationToken: cancellationToken);
        var rows = await dbConnection.ExecuteAsync(command);
        if (rows == 0)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
    }

    public async Task DeleteByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        const string sqlQuery =
            """
            update file_metadata
            set deleted_at = @DeletedAt
            where id = @Id
            """;
        var command = new CommandDefinition(
            sqlQuery,
            new
            {
                DeletedAt = dateTimeOffsetProvider.GetUtcNow(),
                Id = fileMetadataId
            },
            cancellationToken: cancellationToken);
        var rows = await dbConnection.ExecuteAsync(command);
        if (rows == 0)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
    }
}
