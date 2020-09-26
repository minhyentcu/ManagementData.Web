using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Repository
{
    public interface IRepository<T, TDto>
        where T : class
        where TDto : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(long obj);
        void Save();
    }
}
