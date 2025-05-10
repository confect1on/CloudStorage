using StronglyTypedIds;

namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(Template.Long, "long-dapper")]
public readonly partial struct UserId;
