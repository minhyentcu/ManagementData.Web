using Management.Entity;
using ManagementData.Service.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Implement
{
    public class DataInsertRepository : GenericRepository<DataInsert, DataInsert>, IDataInserRepository
    {
        private readonly ManagementDataContext _context;
        public DataInsertRepository(ManagementDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DataInsert>> GetdataFilter(int top, string userId)
        {
            var listDatas = await _context.Database
                   .SqlQuery<DataInsert>("Proc_Paging_demo @userId, @top", new SqlParameter { ParameterName = "userId", Value = userId }, new SqlParameter { ParameterName = "top", Value = top }).ToListAsync();
            return listDatas;
        }
    }
}
