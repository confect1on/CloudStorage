namespace AVScannerService.Persistence;

internal sealed class PersistenceSettings
{
    public required string PostgresConnectionString { get; init; }
}
