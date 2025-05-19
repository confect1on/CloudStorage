namespace CloudStorage.FileService.Domain.Abstractions;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}
