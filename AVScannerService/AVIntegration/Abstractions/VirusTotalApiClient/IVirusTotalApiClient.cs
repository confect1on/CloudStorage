namespace AVScannerService.AVIntegration.Abstractions.VirusTotalApiClient;

public interface IVirusTotalApiClient
{
    Task<GetUrlForLargeFilesDto> GetUrlForLargeFilesAsync(CancellationToken cancellationToken = default);
    
    Task<AnalysisIdDto> UploadFileAsync(
        Stream fileStream,
        Uri? uploadFileUrl = null,
        CancellationToken cancellationToken = default);

    Task<AnalysisDto> GetAnalysisAsync();
}
