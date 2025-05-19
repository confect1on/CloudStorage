using StronglyTypedIds;

namespace CloudStorage.FileService.Domain.FileManagement.ValueObjects;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct FileMetadataId;
