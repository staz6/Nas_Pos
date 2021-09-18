using System;
using System.Collections;
using System.Threading.Tasks;
using API.Interface;
using Nas_Pos.Data;
using Nas_Pos.Entities.Identity;
using Nas_Pos.Interface;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }  

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseClass
        {
            if(_repositories == null) _repositories = new Hashtable();
            var type =  typeof(T).Name;
            if(!_repositories.ContainsKey(type))
            {
                var respositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.
                    CreateInstance(respositoryType.MakeGenericType(typeof(T)),_context);
                _repositories.Add(type,repositoryInstance);
            }
            return (IGenericRepository<T>) _repositories[type];
        }
    }
}