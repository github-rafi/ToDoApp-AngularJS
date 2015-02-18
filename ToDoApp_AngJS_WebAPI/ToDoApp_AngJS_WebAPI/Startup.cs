using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoApp_AngJS_WebAPI.Startup))]
namespace ToDoApp_AngJS_WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
