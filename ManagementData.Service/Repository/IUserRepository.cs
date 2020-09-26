using Management.Entity;
using ManagementData.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser, UserViewModel>
    {
    }
}
