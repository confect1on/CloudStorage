namespace CloudStorage.BackgroundServices.OutboxPublisher;

public record OutboxPublisherSettings
{
    public int BatchSize { get; init; }
}
