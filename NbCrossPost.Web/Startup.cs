using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NbCrossPost.Web.Startup))]
namespace NbCrossPost.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
