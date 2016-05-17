using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(miPT_Project_6.Startup))]
namespace miPT_Project_6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
