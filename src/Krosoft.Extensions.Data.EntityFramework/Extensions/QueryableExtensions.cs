using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Core.Models.Exceptions;
using Microsoft.EntityFrameworkCore; 

namespace Krosoft.Extensions.Data.EntityFramework.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> SortBy<T>(this IQueryable<T> query,
                                          IPaginationRequest request)
    {
        if (request.SortBy != null)
        {
            var sort = request.SortBy.ToArray();

            if (sort.Length > 0)
            {
                foreach (var sortOption in sort)
                {
                    var parts = sortOption.Split(':');
                    if (parts.Length == 2)
                    {
                        var key = parts[0];
                        var order = parts[1].ToLower();
                        var prop = typeof(T).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (prop is null)
                        {
                            throw new KrosoftTechnicalException($"Impossible de déterminer la colonne à partir de la clé suivante : {key}");
                        }

                        // Créer une expression pour le tri.
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var propertyAccess = Expression.MakeMemberAccess(parameter, prop);
                        var orderByExp = Expression.Lambda(propertyAccess, parameter);
                        // Appliquer le tri.
                        var methodName = order == "asc" ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
                        var resultExp = Expression.Call(typeof(Queryable), methodName, new[] { typeof(T), prop.PropertyType }, query.Expression, orderByExp);
                        query = query.Provider.CreateQuery<T>(resultExp);
                    }
                }
            }
        }

        return query;
    }

    public static async Task<PaginationResult<TOuput>> ToPaginationAsync<TEntity, TOuput>(this IQueryable<TEntity> query,
                                                                                          IPaginationRequest request,
                                                                                          AutoMapper.IConfigurationProvider configurationProvider,
                                                                                          CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
                          .ProjectTo<TOuput>(configurationProvider)
                          .SortBy(request)
                          .Skip((request.PageNumber - 1) * request.PageSize)
                          .Take(request.PageSize)
                          .ToListAsync(cancellationToken);

        return new PaginationResult<TOuput>(items, totalCount, request.PageNumber, request.PageSize);
    }

    public static async Task<PaginationResult<T>> ToPaginationAsync<T>(this IQueryable<T> query,
                                                                       IPaginationRequest request,
                                                                       CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
                          .Skip((request.PageNumber - 1) * request.PageSize)
                          .Take(request.PageSize)
                          .ToListAsync(cancellationToken);

        return new PaginationResult<T>(items, totalCount, request.PageNumber, request.PageSize);
    }
}