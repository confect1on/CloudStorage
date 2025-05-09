namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Long, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public partial struct StorageQuotaId;
