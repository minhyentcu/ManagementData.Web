using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementData.Service.ViewModel
{
   public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public DateTime CreateDate { get; set; }
        public string Token { get; set; }
    }
}
