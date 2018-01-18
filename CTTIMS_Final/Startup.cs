using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBFirstSession.Startup))]
namespace DBFirstSession
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
