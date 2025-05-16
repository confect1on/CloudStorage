namespace CloudStorage.Infrastructure.Persistence;

public record DalSettings
{
    public required string PostgresConnectionString { get; init; }
    
};
