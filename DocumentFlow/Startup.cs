using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocumentFlow.Startup))]
namespace DocumentFlow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
