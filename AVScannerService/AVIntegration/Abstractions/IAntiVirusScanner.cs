namespace AVScannerService.AVIntegration.Abstractions;

public interface IAntiVirusScanner
{
    public Task<CheckResponse> CheckAsync();
}
