using System.Linq;
using Microsoft.EntityFrameworkCore;
using Nas_Pos.Entities.Identity;
using Nas_Pos.Specification;

namespace Nas_Pos.Data.Identity
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseClass
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria!=null)
            {
                query=query.Where(spec.Criteria);
            }
            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDescending != null)
            {
                query=query.OrderByDescending(spec.OrderByDescending);
            }
            if(spec.IsPagingEnabled)
            {
                query =query.Skip(spec.Skip).Take(spec.Take);
            }
            query=spec.Includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
        }
        
    }
}