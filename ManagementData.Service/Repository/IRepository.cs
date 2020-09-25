using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(long obj);
        void Save();
    }
}
