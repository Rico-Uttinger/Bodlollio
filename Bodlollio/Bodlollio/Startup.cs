using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bodlollio.Startup))]
namespace Bodlollio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
