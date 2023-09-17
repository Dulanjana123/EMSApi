using EMSApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> Employee { get; }
        //IGenericRepository<User> User { get; }
        Task Save();
    }
}
