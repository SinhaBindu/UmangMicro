using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UmangMicro.Startup))]
namespace UmangMicro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
