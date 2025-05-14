using StronglyTypedIds;

namespace CloudStorage.Domain.UserManagement.ValueObjects;

[StronglyTypedId(Template.Long, "long-dapper")]
public readonly partial struct UserId;
