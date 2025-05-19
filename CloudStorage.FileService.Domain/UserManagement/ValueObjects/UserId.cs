using StronglyTypedIds;

namespace CloudStorage.FileService.Domain.UserManagement.ValueObjects;

[StronglyTypedId(Template.Long, "long-dapper")]
public readonly partial struct UserId;
