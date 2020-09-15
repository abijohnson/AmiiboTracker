using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmiiboTracker.Startup))]
namespace AmiiboTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
