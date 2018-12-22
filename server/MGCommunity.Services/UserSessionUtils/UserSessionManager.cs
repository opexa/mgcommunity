namespace MGCommunity.Services.UserSessionUtils
{
	using Data;
	using MGCommunity.Models;
	using Microsoft.AspNet.Identity;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Web;

	public class UserSessionManager
	{
		protected IMGCommunityData Data { get; private set; }

		private TimeSpan UserSessionTimeout = System.TimeSpan.FromHours(1);

		public UserSessionManager(IMGCommunityData data)
		{
			this.Data = data;
		}

		public UserSessionManager() : this(new MGCommunityData()) { }

		private HttpRequestMessage CurrentRequest
		{
			get
			{
				return (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
			}
		}

		/// <returns>The current bearer authorization token from the HTTP headers</returns>
		private string GetCurrentBearerAuthrorizationToken()
		{
			string authToken = null;
			if (CurrentRequest.Headers.Authorization != null)
			{
				if (CurrentRequest.Headers.Authorization.Scheme == "Bearer")
				{
					authToken = CurrentRequest.Headers.Authorization.Parameter;
				}
			}
			return authToken;
		}

		private string GetCurrentUserId()
		{
			if (HttpContext.Current.User == null)
			{
				return null;
			}
			string userId = HttpContext.Current.User.Identity.GetUserId();
			return userId;
		}

		/// <summary>
		/// Extends the validity period of the current user's session in the database.
		/// This will configure the user's bearer authorization token to expire after
		/// certain period of time (e.g. 30 minutes, see UserSessionTimeout in Web.config)
		/// </summary>
		public void CreateUserSession(string username, string authToken)
		{
			var userId = this.Data.Users.All().First(u => u.UserName == username).Id;
			var userSession = new UserSession()
			{
				OwnerUserId = userId,
				AuthToken = authToken
			};
			this.Data.UserSessions.Add(userSession);

			// Extend the lifetime of the current user's session: current moment + fixed timeout
			userSession.ExpirationDateTime = DateTime.Now + UserSessionTimeout;
			this.Data.SaveChanges();
		}

		/// <summary>
		/// Makes the current user session invalid (deletes the session token from the user sessions).
		/// The goal is to revoke any further access with the same authorization bearer token.
		/// Typically this method is called at "logout".
		/// </summary>
		public void InvalidateUserSession()
		{
			string authToken = GetCurrentBearerAuthrorizationToken();
			var currentUserId = GetCurrentUserId();
			var userSession = this.Data.UserSessions.All().FirstOrDefault(session =>
					session.AuthToken == authToken && session.OwnerUserId == currentUserId);
			if (userSession != null)
			{
				this.Data.UserSessions.Delete(userSession);
				this.Data.SaveChanges();
			}
		}

		/// <summary>
		/// Re-validates the user session. Usually called at each authorization request.
		/// If the session is not expired, extends it lifetime and returns true.
		/// If the session is expired or does not exist, return false.
		/// </summary>
		/// <returns>true if the session is valid</returns>
		public bool ReValidateSession()
		{
			string authToken = this.GetCurrentBearerAuthrorizationToken();
			var currentUserId = this.GetCurrentUserId();
			var userSession = this.Data.UserSessions.All().FirstOrDefault(session =>
					session.AuthToken == authToken && session.OwnerUserId == currentUserId);

			if (userSession == null)
			{
				// User does not have a session with this token --> invalid session
				return false;
			}

			if (userSession.ExpirationDateTime < DateTime.Now)
			{
				// User's session is expired --> invalid session
				return false;
			}

			// Extend the lifetime of the current user's session: current moment + fixed timeout
			userSession.ExpirationDateTime = DateTime.Now + UserSessionTimeout;
			this.Data.SaveChanges();

			return true;
		}

		public void DeleteExpiredSessions()
		{
			var userSession = this.Data.UserSessions.All().FirstOrDefault(s => s.ExpirationDateTime < DateTime.Now);
			if (userSession != null)
				this.Data.UserSessions.Delete(userSession.Id);
		}
	}
}