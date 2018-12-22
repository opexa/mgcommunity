using MGCommunity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MGCommunity.Services.UserSessionUtils
{
	public class SessionAuthorizeAttribute : AuthorizeAttribute
	{
		protected IMGCommunityData Data { get; private set; }

		public SessionAuthorizeAttribute(IMGCommunityData data)
		{
			this.Data = data;
		}

		public SessionAuthorizeAttribute()
				: this(new MGCommunityData())
		{
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (SkipAuthorization(actionContext))
			{
				return;
			}

			var userSessionManager = new UserSessionManager();
			if (userSessionManager.ReValidateSession())
			{
				base.OnAuthorization(actionContext);
			}
			else
			{
				actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
						HttpStatusCode.Unauthorized, "Session token expried or not valid.");
			}
		}

		private static bool SkipAuthorization(HttpActionContext actionContext)
		{
			return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
						 || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
		}
	}
}