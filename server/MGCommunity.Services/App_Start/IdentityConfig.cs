namespace MGCommunity.Services.App_Start
{
	using MGCommunity.Data;
	using MGCommunity.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;
	using Microsoft.Owin;

	public class ApplicationUserManager : UserManager<User>
	{
		public ApplicationUserManager(IUserStore<User> store)
				: base(store)
		{
			// Configure validation logic for usernames
			this.UserValidator = new UserValidator<User>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			this.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 2,
				RequireNonLetterOrDigit = false,
				RequireDigit = false,
				RequireLowercase = false,
				RequireUppercase = false,
			};
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var manager = new ApplicationUserManager(new UserStore<User>(context.Get<MGCContext>()));
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider = new DataProtectorTokenProvider<User>(
						dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}
	}
}