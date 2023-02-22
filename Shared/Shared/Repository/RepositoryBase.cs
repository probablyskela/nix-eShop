using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Repository.Abstractions;

namespace Shared.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class
{
    protected readonly DbContext RepositoryContext;

    protected RepositoryBase(DbContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return !trackChanges
            ? RepositoryContext.Set<T>()
                .AsNoTracking()
            : RepositoryContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return !trackChanges
            ? RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
            : RepositoryContext.Set<T>()
                .Where(expression);
    }

    public void Create(T entity) => RepositoryContext.Add(entity);

    public void Update(T entity) => RepositoryContext.Update(entity);

    public void Delete(T entity) => RepositoryContext.Remove(entity);
}