using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RiskApplication.WebApi.Startup))]
namespace RiskApplication.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
