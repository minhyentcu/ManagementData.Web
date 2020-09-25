using Management.Entity;
using ManagementData.Service.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Implement
{
    public class GenericRepository<T> : IDisposable, IRepository<T> where T : class
    {
        public ManagementDataContext _context = null;
        public DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new ManagementDataContext();
            this._context.Configuration.ValidateOnSaveEnabled = false;
            this.table = _context.Set<T>();
        }
        public GenericRepository(ManagementDataContext _context)
        {
            this._context = _context;
            this.table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(long id)
        {
            return table.Find(id);
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
        public void Delete(long id)
        {
            var data = table.Find(id);
            table.Remove(data);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
