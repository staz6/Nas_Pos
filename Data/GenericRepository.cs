using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nas_Pos.Data.Identity;
using Nas_Pos.Entities.Identity;
using Nas_Pos.Interface;
using Nas_Pos.Specification;

namespace Nas_Pos.Data
{
        public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
         private readonly AppDbContext _context;
        private DbSet<T> table = null;
        
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async  Task<IReadOnlyList<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }

         public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsyncWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}