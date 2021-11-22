using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElevenNoteDemo.WebMVC.Startup))]
namespace ElevenNoteDemo.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
