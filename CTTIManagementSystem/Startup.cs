using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CTTIManagementSystem.Startup))]
namespace CTTIManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
