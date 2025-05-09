namespace CloudStorage.Infrastructure.DataAccess;

public record DalOptions
{
    public required string PostgresConnectionString { get; init; }
    
};
