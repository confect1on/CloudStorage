namespace CloudStorage.Domain.Abstractions;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}
