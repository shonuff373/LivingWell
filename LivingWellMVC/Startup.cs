using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LivingWellMVC.Startup))]
namespace LivingWellMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
