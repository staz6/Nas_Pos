using System.Collections.Generic;
using System.Threading.Tasks;
using Nas_Pos.Entities.Identity;
using Nas_Pos.Specification;

namespace Nas_Pos.Interface
{
    public interface IGenericRepository<T> where T: BaseClass
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(object id);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsyncWithSpec(ISpecification<T> spec);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        Task Save();

        
        
    }
}