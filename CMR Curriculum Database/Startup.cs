using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMR_Curriculum_Database.Startup))]
namespace CMR_Curriculum_Database
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
