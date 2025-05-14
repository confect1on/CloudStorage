namespace CloudStorage.Domain.Entities;

public enum OutboxStatus
{
    Pending,
    Processing,
    Success,
    Failure,
}
