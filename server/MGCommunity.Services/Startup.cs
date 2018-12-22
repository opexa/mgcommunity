using Microsoft.Owin;

[assembly: OwinStartup(typeof(MGCommunity.Services.Startup))]
namespace MGCommunity.Services
{
	using Microsoft.Owin.Security.OAuth;
	using Owin;
	using System.Web.Http;

	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

			ConfigureAuth(app);

			GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
		}
	}
}