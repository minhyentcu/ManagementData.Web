using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagementData.Web.Startup))]
namespace ManagementData.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
