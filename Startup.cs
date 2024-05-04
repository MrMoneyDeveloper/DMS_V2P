using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DMS_V2P.Startup))]
namespace DMS_V2P
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
