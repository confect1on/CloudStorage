using StronglyTypedIds;

namespace CloudStorage.FileService.Domain.UserManagement.ValueObjects;

[StronglyTypedId(Template.Guid, "guid-dapper")]
public readonly partial struct UserId;
