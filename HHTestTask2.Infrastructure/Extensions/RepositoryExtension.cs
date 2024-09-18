using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, IEnumerable<string> entitiesToInclude) where TEntity : class
        {
            if (entitiesToInclude != null && entitiesToInclude.Any())
                foreach (var entity in entitiesToInclude)
                    source = source.Include(entity);

            return source;
        }

        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, IEnumerable<Expression<Func<TEntity, bool>>> predicates) where TEntity : class
        {
            if (predicates != null && predicates.Any())
                foreach (var predicate in predicates)
                    source = source.Where(predicate);

            return source;
        }
    }
}
