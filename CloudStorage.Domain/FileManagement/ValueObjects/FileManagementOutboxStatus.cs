namespace CloudStorage.Domain.FileManagement.ValueObjects;

public enum FileManagementOutboxStatus
{
    Pending,
    Processing,
    Success,
    Failure,
}
