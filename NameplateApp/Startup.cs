using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Hosting;

[assembly: OwinStartup(typeof(NameplateApp.Startup))]

namespace NameplateApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Mensaje para confirmar que OWIN arranca
            System.Diagnostics.Debug.WriteLine("OWIN se está ejecutando.");

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }

        
    }
}
