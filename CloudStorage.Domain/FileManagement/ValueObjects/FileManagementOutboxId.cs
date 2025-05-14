using StronglyTypedIds;

namespace CloudStorage.Domain.FileManagement.ValueObjects;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct FileManagementOutboxId;
