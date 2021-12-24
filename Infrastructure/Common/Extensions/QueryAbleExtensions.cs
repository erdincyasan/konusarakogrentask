using Application.Common.Specifications;
using Domain;
using Domain.Common.Contracts;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec)
    where T : BaseEntity
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(query, (current, include) => current.Include(include));
        var secondaryResult = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));
        if (spec.Criteria == null)
            return secondaryResult;
        else
            return secondaryResult.Where(spec.Criteria);
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Filters<T> filters)
    {
        if (filters?.IsValid() == true)
            query = filters.Get().Aggregate(query, (current, filter) => current.Where(filter.Expression));
        return query;
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string[] orderBy)
        where T : BaseEntity
    {
        string ordering = new OrderByConverter().ConvertBack(orderBy);
        
        return query.OrderBy(a => a.Id);

        // TODO
       // return !string.IsNullOrWhiteSpace(ordering) ? query.OrderBy(ordering) : query.OrderBy(a => a.Id);
    }
}
