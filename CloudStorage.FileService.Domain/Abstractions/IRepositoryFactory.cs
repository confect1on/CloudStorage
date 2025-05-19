using System.Data;

namespace CloudStorage.FileService.Domain.Abstractions;

public interface IRepositoryFactory<out T> where T : class
{
    T Create(IDbConnection dbConnection, IDbTransaction? transaction = null);

    T Create();
}
