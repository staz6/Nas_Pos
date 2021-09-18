using System;
using System.Threading.Tasks;
using Nas_Pos.Entities.Identity;
using Nas_Pos.Interface;

namespace API.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseClass;
        Task<int> Complete();
    }
}