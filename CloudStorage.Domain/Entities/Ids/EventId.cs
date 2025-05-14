using StronglyTypedIds;

namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct EventId;
