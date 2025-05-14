using StronglyTypedIds;

namespace CloudStorage.Domain;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct EventId;
