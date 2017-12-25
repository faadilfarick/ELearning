using Microsoft.Owin;
using Owin;
using ELearning;

//[assembly: OwinStartupAttribute(typeof(ELearning.Startup))] //this is the original one
[assembly: OwinStartup(typeof(ELearning.Startup))] //this is for signalR but both works fine
namespace ELearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();

            //ConfigureAuth(app); 
            //Commenting this will take of user authentication..This is the major problem here if this part is uncommented
            //signalR will not get the name
            //nor display chat messages

            
        }
    }
}
