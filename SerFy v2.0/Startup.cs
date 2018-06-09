using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SerFy_v2._0.Startup))]
namespace SerFy_v2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
