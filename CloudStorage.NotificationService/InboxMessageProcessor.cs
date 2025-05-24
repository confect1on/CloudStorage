using CloudStorage.NotificationService.Domain.NotificationManagement;
using CloudStorage.NotificationService.Domain.NotificationManagement.ValueObjects;
using CloudStorage.NotificationService.Infrastructure.Persistence.Repositories;
using CloudStorage.NotificationService.Notifications.EmailNotificationService.Dtos;
using CloudStorage.NotificationService.Persistence.Repositories;

namespace CloudStorage.NotificationService;

internal sealed class InboxMessageProcessor(
    IFilePublishedInboxRepositoryFactory filePublishedInboxRepositoryFactory,
    INotificationService notificationService) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var filePublishedInboxRepository = filePublishedInboxRepositoryFactory.Create();
            var filePublishedInboxDto = await filePublishedInboxRepository.GetFirstUnprocessedWithLeaseAsync(stoppingToken);
            var eventNotificationDto = new EventNotificationDto(new EventDto(EventType.FilePublished, filePublishedInboxDto.FileMetadataId));
            await notificationService.SendNotificationAsync(eventNotificationDto, stoppingToken);
            await filePublishedInboxRepository.UpdateProcessedAt(
                eventNotificationDto.EventDto.FileMetadataId,
                stoppingToken);
        }
    }
}
