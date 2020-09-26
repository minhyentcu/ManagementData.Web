using Management.Entity;
using ManagementData.Service.Repository;
using ManagementData.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.Implement
{
   public class UserRepository :GenericRepository<ApplicationUser,UserViewModel>, IUserRepository
    {

    }
}
