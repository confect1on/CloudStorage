using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;
using CloudStorage.Domain.Exceptions;
using Dapper;

namespace CloudStorage.Infrastructure.DataAccess;

internal sealed class FileMetadataRepository(DalOptions dalOptions) : PostgresRepository(dalOptions), IFileMetadataRepository
{
    public async Task<FileMetadataId> AddAsync(FileMetadata fileMetadata, CancellationToken cancellationToken = default)
    {
        await using var connection = await GetConnectionAsync();
        const string sqlQuery =
            """
            insert into file_metadata (id, user_id, storage_id, file_name, file_size_in_bytes, mime_type, created_at)
            values (@Id, @UserId, @StorageId, @FileName, @FileSizeInBytes, @MimeType, @CreatedAt);
            """;
        var fileMetaDataId = await connection.QueryFirstAsync<FileMetadataId>(sqlQuery, fileMetadata);
        return fileMetaDataId;
    }

    public async Task<FileMetadata> GetByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        await using var connection = await GetConnectionAsync();
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
            cancellationToken: cancellationToken);
        var fileMetadata = await connection.QueryFirstOrDefaultAsync<FileMetadata>(commandDefinition);
        if (fileMetadata is null)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
        return fileMetadata;
    }

    public async Task AttachStorageIdAsync(FileMetadataId fileMetadataId, StorageId storageId, CancellationToken cancellationToken = default)
    {
        await using var connection = await GetConnectionAsync();
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
            cancellationToken: cancellationToken);
        var rows = await connection.ExecuteAsync(command);
        if (rows == 0)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
    }

    public async Task DeleteByIdAsync(FileMetadataId fileMetadataId, CancellationToken cancellationToken = default)
    {
        await using var connection = await GetConnectionAsync();
        const string sqlQuery =
            """
            delete from file_metadata
            where id = @Id;
            """;
        var command = new CommandDefinition(
            sqlQuery,
            new
            {
                Id = fileMetadataId
            },
            cancellationToken: cancellationToken);
        var rows = await connection.ExecuteAsync(command);
        if (rows == 0)
        {
            throw new FileMetadataNotFoundException(fileMetadataId);
        }
    }
}
