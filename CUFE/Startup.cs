using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CUFE.Startup))]
namespace CUFE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
