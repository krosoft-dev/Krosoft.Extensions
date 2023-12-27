using Krosoft.Extensions.Data.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Interfaces;

public interface IDbContextScope<T> : IReadDbContextScope<T>
{
    public IUnitOfWork GetUnitOfWork();

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class;
}