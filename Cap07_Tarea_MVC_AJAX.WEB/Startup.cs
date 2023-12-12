using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cap07_Tarea_MVC_AJAX.WEB.Startup))]
namespace Cap07_Tarea_MVC_AJAX.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
