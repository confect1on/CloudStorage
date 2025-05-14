namespace CloudStorage.Infrastructure.DataAccess.Persistence;

public record DalSettings
{
    public required string PostgresConnectionString { get; init; }
    
};
