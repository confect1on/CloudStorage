namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public partial struct StorageId;
