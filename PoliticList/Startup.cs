using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoliticList.Startup))]
namespace PoliticList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
