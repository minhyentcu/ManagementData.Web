﻿using Management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Repository
{
    public interface IDataInserRepository
    {
        Task<IEnumerable<DataInsert>> GetdataFilter(int top, string userId);
    }
}
