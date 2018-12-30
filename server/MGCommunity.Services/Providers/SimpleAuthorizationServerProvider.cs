using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using MGCommunity.Models;
using MGCommunity.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using MGCommunity.Services.App_Start;
using MGCommunity.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;

namespace Saver.Services.Providers
{
	public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
	{
		private readonly string publicClientId;

		public ApplicationOAuthProvider(string publicClientId)
		{
			if (publicClientId == null)
			{
				throw new ArgumentNullException("publicClientId");
			}

			this.publicClientId = publicClientId;
		}

		public static AuthenticationProperties CreateProperties(string username, string role, string avatar)
		{
			IDictionary<string, string> data = new Dictionary<string, string>
			{
				{ "username", username },
				{ "role", role },
				{ "avatar", avatar }
			};
			return new AuthenticationProperties(data);
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var userManager = new ApplicationUserManager(
					new UserStore<User>(new MGCContext()));

			var user = await userManager.FindAsync(context.UserName, context.Password);

			if (user == null)
			{
				context.SetError("invalid_grant", "Грешно потребителско име или парола.");
				return;
			}

			var oauthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
			var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

			string userRole = "";
			using (var dbContext = new MGCContext())
			{
				userRole = dbContext.Roles.Find(user.Roles.First().RoleId).Name;				
			}

			AuthenticationProperties properties = CreateProperties(user.UserName, userRole, user.Avatar);
			var ticket = new AuthenticationTicket(oauthIdentity, properties);
			context.Validated(ticket);
			context.Request.Context.Authentication.SignIn(cookiesIdentity);
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (var property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientAuthentication(
				OAuthValidateClientAuthenticationContext context)
		{
			// Resource owner password credentials does not provide a client ID.
			if (context.ClientId == null)
			{
				context.Validated();
			}

			return Task.FromResult<object>(null);
		}

		public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
		{
			if (context.ClientId == this.publicClientId)
			{
				var expectedRootUri = new Uri(context.Request.Uri, "/");

				if (expectedRootUri.AbsoluteUri == context.RedirectUri)
				{
					context.Validated();
				}
			}

			return Task.FromResult<object>(null);
		}
	}
}