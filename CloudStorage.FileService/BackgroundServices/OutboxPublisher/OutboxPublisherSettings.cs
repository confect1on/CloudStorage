namespace CloudStorage.FilesService.BackgroundServices.OutboxPublisher;

public record OutboxPublisherSettings
{
    public int BatchSize { get; init; }
}
