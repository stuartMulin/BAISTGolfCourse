using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BAISTGOLF.COM.Startup))]
namespace BAISTGOLF.COM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
