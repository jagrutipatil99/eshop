using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eshop.Startup))]
namespace eshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
