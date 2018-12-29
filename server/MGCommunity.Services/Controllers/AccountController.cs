namespace MGCommunity.Services.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Web;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Http;
	using System.Web.Script.Serialization;

	using Microsoft.Owin.Testing;
	using Microsoft.Owin.Security;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;

	using MGCommunity.Data;
	using MGCommunity.Models;
	using MGCommunity.Services.App_Start;
	using Models.BindingModels;
	using UserSessionUtils;
	using System.Linq;

	[RoutePrefix("api/Account")]
	public class AccountController : BaseApiController
	{
		private ApplicationUserManager userManager;

		public AccountController(IMGCommunityData data) : base(data) { }

		public AccountController()
            : base(new MGCommunityData())
        {
			this.userManager = new ApplicationUserManager(new UserStore<User>(new MGCContext()));
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return this.userManager;
			}
		}

		private IAuthenticationManager Authentication
		{
			get
			{
				return Request.GetOwinContext().Authentication;
			}
		}

		// POST api/Account/Register
		[HttpPost]
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(RegisterBindingModel model)
		{
			if (model == null)
				return this.BadRequest("Невалидни данни.");
			
			if (!ModelState.IsValid)
				return BadRequest(this.ModelState);

			bool alreadyExisting = this.Data.Users.All().Any(u => u.UserName == model.Username);
			if (alreadyExisting)
			{
				ModelState.AddModelError("error", "Съществува акаунт с такова потребителско име");
				return this.BadRequest(ModelState);
			}

			User user = new User
			{
				UserName = model.Username,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				StartYear = model.StartYear,
				Avatar = this.DomainUrl + "/Files/Avatars/generic.jpg",
				Class = (UserClass)model.Class,
				Status = "Ученик"
			};

			IdentityResult identityResult = await this.UserManager.CreateAsync(user, model.Password);

			if (!identityResult.Succeeded)
			{
				return this.GetErrorResult(identityResult);
			}

			userManager.AddToRole(user.Id, "Student");

			IHttpActionResult errorResult = GetErrorResult(identityResult);

			var loginResult = await this.Login(new LoginUserBindingModel()
			{
				Username = model.Username,
				Password = model.Password
			});
			return loginResult;
		}

		// POST api/Account/Login
		[HttpPost]
		[AllowAnonymous]
		[Route("Login")]
		public async Task<IHttpActionResult> Login(LoginUserBindingModel model)
		{
			if (model == null)
			{
				return this.BadRequest("Invalid user data");
			}

			// Invoke the "token" OWIN service to perform the login (POST /api/token)
			// Use Microsoft.Owin.Testing.TestServer to perform in-memory HTTP POST request
			var testServer = TestServer.Create<Startup>();
			var requestParams = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("grant_type", "password"),
				new KeyValuePair<string, string>("username", model.Username),
				new KeyValuePair<string, string>("password", model.Password)
			};
			var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
			var tokenServiceResponse = await testServer.HttpClient.PostAsync(Startup.TokenEndpointPath, requestParamsFormUrlEncoded);
			var jsSerializer = new JavaScriptSerializer();

			var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
			var responseData = jsSerializer.Deserialize<Dictionary<string, string>>(responseString);

			if (tokenServiceResponse.StatusCode == HttpStatusCode.OK)
			{
				// Sucessful login --> create user session in the database
				var authToken = responseData["access_token"];
				var username = responseData["username"];
				var userSessionManager = new UserSessionManager();
				userSessionManager.CreateUserSession(username, authToken);

				// Cleanup: delete expired sessions fromthe database
				userSessionManager.DeleteExpiredSessions();
			}
			return this.ResponseMessage(tokenServiceResponse);
		}

		// POST api/Account/Logout
		[HttpPost]
		[SessionAuthorize]
		[Route("Logout")]
		public IHttpActionResult Logout()
		{
			// This does not actually perform logout! The OWIN OAuth implementation
			// does not support "revoke OAuth token" (logout) by design.
			this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalBearer);

			// Delete the user's session from the database (revoke its bearer token)
			var userSessionManager = new UserSessionManager();
			userSessionManager.InvalidateUserSession();

			return this.Ok(new { message = "Отписахте се успешно." });
		}

		// PUT api/Account/ChangePassword
		[HttpPut]
		[Route("ChangePassword")]
		public async Task<IHttpActionResult> ChangeAccountPassword(ChangePasswordBindingModel model)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			if (User.Identity.GetUserName() == "admin")
				return this.BadRequest("Администраторът не може да сменя паролата си.");

			IdentityResult result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

			if (!result.Succeeded)
				return this.GetErrorResult(result);

			return this.Ok(new { message = "Успешно смени паролата си." });
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("error", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}

	public class AuthRepository : IDisposable
	{
		private MGCContext context;

		private UserManager<User> userManager;

		public AuthRepository()
		{
			context = new MGCContext();
			userManager = new UserManager<User>(new UserStore<User>(context));
		}

		public async Task<IdentityResult> RegisterUser(RegisterBindingModel userModel)
		{
			User user = new User
			{
				UserName = userModel.Username,
				Email = userModel.Email,
				Class = (UserClass)userModel.Class,
				StartYear = userModel.StartYear
			};

			var result = await userManager.CreateAsync(user, userModel.Password);

			return result;
		}

		public async Task<User> FindUser(string userName, string password)
		{
			User user = await userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			context.Dispose();
			userManager.Dispose();
		}
	}
}
