using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Associazione_Musicale.Startup))]
namespace Associazione_Musicale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
