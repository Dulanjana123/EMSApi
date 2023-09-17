using EMSApi.Data;
using EMSApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EMSDBContext _context;

        private IGenericRepository<Employee> _employee;
        //private IGenericRepository<User> _user;

        public UnitOfWork(EMSDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<Employee> Employee => _employee ??= new GenericRepository<Employee>(_context);

        //public IGenericRepository<User> User => _user ??= new GenericRepository<User>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
