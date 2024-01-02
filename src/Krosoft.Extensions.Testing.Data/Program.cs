using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Positive.Extensions.Cqrs.Models.Commands;
using Positive.Extensions.Cqrs.Models.Queries;
using Positive.Extensions.Data.Abstractions.Interfaces;
using Positive.Extensions.Data.Abstractions.Models;
using Positive.Extensions.Data.EntityFramework.Contexts;

namespace Positive.Extensions.Testing;

public abstract class BaseTest
{
    Krosoft.Extensions.Testing.Data.EntityFramework

    protected IQueryable<T> GetDb<T>(IServiceProvider container) where T : Entity
    {
        var context = container.GetRequiredService<IReadRepository<T>>();

        return context.Query();
    }

    protected void InitDb<TDbContext, T>(IServiceProvider container,
                                         T entity) where TDbContext : PositiveContext where T : Entity
    {
        InitDb<TDbContext, T>(container, new List<T> { entity });
    }

    protected void InitDb<TDbContext, T>(IServiceProvider container,
                                         IEnumerable<T> entities) where TDbContext : PositiveContext where T : Entity
    {
        var context = container.GetRequiredService<TDbContext>();

        context.AddRange(entities);
        context.SaveChanges();
    }





}