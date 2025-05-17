using AVScannerService.AVIntegration.Abstractions;
using AVScannerService.AVIntegration.Abstractions.VirusTotalApiClient;

namespace AVScannerService.AVIntegration.VirusTotalIntegration;

internal sealed class VirusTotalScanner(IVirusTotalApiClient virusTotalApiClient) : IAntiVirusScanner
{
    private const int MaxUrlUploadSize = 1024 * 1024 * 650; // 650 MB
    private const int MaxDirectUploadSize = 1024 * 1024 * 32; // 32 MB
    public async Task<CheckResponse> CheckAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream.Length > MaxUrlUploadSize)
        {
            throw new NotSupportedException("Virus total upload size is too big.");
        }
        AnalysisIdDto analysisIdDto;
        if (stream.Length > MaxDirectUploadSize)
        {
            analysisIdDto = await CheckLargeFileAsync(stream, cancellationToken);
        }
        else
        {
            analysisIdDto = await CheckSmallFileAsync(stream, cancellationToken);
        }
        throw new InvalidOperationException();
    }

    private async Task<AnalysisIdDto> CheckSmallFileAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var analysisId = await virusTotalApiClient.UploadFileAsync(stream, cancellationToken: cancellationToken); 
        return analysisId;
    }

    private async Task<AnalysisIdDto> CheckLargeFileAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var getUrlForLargeFilesDto = await virusTotalApiClient.GetUrlForLargeFilesAsync(cancellationToken);
        var urlForUploading = new Uri(getUrlForLargeFilesDto.Data);
        var analysisId = await virusTotalApiClient.UploadFileAsync(stream, urlForUploading, cancellationToken);
        return analysisId;
    }

    public Task<CheckResponse> CheckAsync()
    {
        throw new NotImplementedException();
    }
}
