
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Entity
{
    public partial class ManagementDataContext : IdentityDbContext<ApplicationUser>
    {
        public ManagementDataContext()
            : base("name=ManagementDBContext")
        {
        }

      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
        public static ManagementDataContext Create()
        {
            return new ManagementDataContext();
        }
    }
}
