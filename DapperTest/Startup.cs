using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DapperTest.Startup))]
namespace DapperTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
