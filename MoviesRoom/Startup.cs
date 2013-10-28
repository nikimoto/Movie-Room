using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesRoom.Startup))]
namespace MoviesRoom
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
