using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankApps.Startup))]
namespace BankApps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
