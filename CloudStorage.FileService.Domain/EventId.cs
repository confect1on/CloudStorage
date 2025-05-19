using StronglyTypedIds;

namespace CloudStorage.FileService.Domain;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct EventId;
