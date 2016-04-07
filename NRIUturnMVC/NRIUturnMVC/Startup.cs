using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NRIUturnMVC.Startup))]
namespace NRIUturnMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
