namespace CloudStorage.Domain.Entities.Ids;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public partial struct FileMetadataId;
