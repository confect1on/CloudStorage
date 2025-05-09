namespace CloudStorage.Infrastructure.DataAccess;

public record DalSettings
{
    public required string PostgresConnectionString { get; init; }
    
};
