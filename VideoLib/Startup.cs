using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoLib.Startup))]
namespace VideoLib
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
