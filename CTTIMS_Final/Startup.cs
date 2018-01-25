using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CTTIMS_Final.Startup))]
namespace CTTIMS_Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
