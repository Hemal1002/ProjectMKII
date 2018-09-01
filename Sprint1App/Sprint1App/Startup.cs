using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sprint1App.Startup))]
namespace Sprint1App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
