namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public struct StorageId;
