using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OAuthDemoMVC5.Startup))]
namespace OAuthDemoMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
