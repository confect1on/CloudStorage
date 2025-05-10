using StronglyTypedIds;

namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(Template.String, "string-dapper")]
public readonly partial struct StorageId;
