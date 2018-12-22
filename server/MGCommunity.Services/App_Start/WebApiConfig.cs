using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace MGCommunity.Services
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{action}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
