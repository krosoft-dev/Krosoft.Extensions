using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.Abstractions.Models;

namespace Krosoft.Extensions.Data.EntityFramework.Repositories;

public class WriteRepository<TEntity> : IWriteRepository<TEntity>
    where TEntity : class

{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public WriteRepository(DbContext dbContext)
    {
        Guard.IsNotNull(nameof(dbContext), dbContext);

        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public void Insert(TEntity entity)
    {
        Guard.IsNotNull(nameof(entity), entity);

        _dbSet.Add(entity);
    }

    public void InsertRange(IEnumerable<TEntity> entities)
    {
        Guard.IsNotNull(nameof(entities), entities);

        _dbSet.AddRange(entities);
    }

    public void DeleteById(params object[] key)
    {
        var entity = Get(key);
        Delete(entity);
    }

    public async Task DeleteByIdAsync(params object[] key)
    {
        var entity = await GetAsync(key);
        Delete(entity);
    }

    public void Delete(TEntity entity)
    {
        Guard.IsNotNull(nameof(entity), entity);

        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public void DeleteRange()
    {
        DeleteRange(_dbSet);
    }

    public void InsertUpdateDelete(CrudBusiness<TEntity> crudBusiness)
    {
        InsertRange(crudBusiness.ToAdd);
        UpdateRange(crudBusiness.ToUpdate);
        DeleteRange(crudBusiness.ToDelete);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        Guard.IsNotNull(nameof(entities), entities);

        _dbSet.RemoveRange(entities);
    }

    public void DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);
        _dbSet.RemoveRange(query);
    }

    public void Update(TEntity entityToUpdate)
    {
        Guard.IsNotNull(nameof(entityToUpdate), entityToUpdate);

        _dbSet.Update(entityToUpdate);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public TEntity Get(params object[] key) => _dbSet.Find(key);

    public ValueTask<TEntity> GetAsync(params object[] key) => _dbSet.FindAsync(key);

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        Guard.IsNotNull(nameof(entities), entities);
        _dbSet.UpdateRange(entities);
    }

    public IQueryable<TEntity> Query() => _dbSet;

    public void Update(TEntity entityToUpdate, params Expression<Func<TEntity, object>>[] propertiesExpression)
    {
        Guard.IsNotNull(nameof(entityToUpdate), entityToUpdate);
        _dbSet.Attach(entityToUpdate);

        foreach (var propertyExpression in propertiesExpression)
        {
            _dbContext.Entry(entityToUpdate).Property(propertyExpression).IsModified = true;
        }
    }

    public void UpdateRange(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertiesExpression)
    {
        foreach (var entity in entities)
        {
            Update(entity, propertiesExpression);
        }
    }
}