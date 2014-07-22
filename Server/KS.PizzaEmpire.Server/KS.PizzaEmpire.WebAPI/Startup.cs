using Microsoft.Owin;
[assembly: OwinStartup(typeof(KS.PizzaEmpire.WebAPI.Startup))]

namespace KS.PizzaEmpire.WebAPI
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
