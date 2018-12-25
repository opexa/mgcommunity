using MGCommunity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MGCommunity.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<MGCContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = false;
		}

		protected override void Seed(MGCContext context)
		{
			SeedRolesAndUsers(context);
			SeedSectionsAndCategories(context);
		}
		
		private User SeedRolesAndUsers(MGCContext context)
		{
			if(!context.Users.Any())
			{
				var userStore = new UserStore<User>(context);
				var userManager = new UserManager<User>(userStore);
				User adminUser = new User
				{
					UserName = "admin",
					FirstName = "Admin",
					LastName = "Admin",
					Email = "admin@mgberon.com"
				};
				var adminUserCreateResult = userManager.Create(adminUser, "mgadmin1298");

				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
				var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
				var roleTeacherCreateResult = roleManager.Create(new IdentityRole("Teacher"));
				var roleUserCreateResult = roleManager.Create(new IdentityRole("Student"));

				if (!roleUserCreateResult.Succeeded)
				{
					throw new Exception(string.Join("; ", roleUserCreateResult.Errors));
				}

				if (!roleCreateResult.Succeeded)
				{
					throw new Exception(string.Join("; ", roleCreateResult.Errors));
				}

				// Add "admin" user to "Administrator" role
				var addAdminRoleResult = userManager.AddToRole(adminUser.Id, "Administrator");
				if (!addAdminRoleResult.Succeeded)
				{
					throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
				}

				context.SaveChanges();
				return adminUser;
			}
			return null;

			// SEED TEACHERS HERE
		}

		private static void SeedSectionsAndCategories(MGCContext context)
		{
			if (!context.Sections.Any())
			{
				Section general = new Section { Name = "Генерални", Visible = true };
				Section groups = new Section { Name = "Групи", Visible = true };
				Section ideas = new Section { Name = "Идеи", Visible = true };
				Section other = new Section { Name = "Други", Visible = true };

				context.Sections.Add(general);
				context.Sections.Add(groups);
				context.Sections.Add(ideas);
				context.Sections.Add(other);
				context.SaveChanges();

				var categories = new List<Category>
				{
					new Category() { Name = "Правила", Section = general, TopicsCount = 0 },
					new Category { Name = "Новини", Section = general, TopicsCount = 0 },
					new Category { Name = "Състезания", Section = general, TopicsCount = 0 },
					new Category { Name = "Предприемачество", Section = groups, TopicsCount = 0 },
					new Category { Name = "Физика и Астрономия", Section = groups, TopicsCount = 0 },
					new Category { Name = "Спорт", Section = groups, TopicsCount = 0 },
					new Category { Name = "Математика", Section = groups, TopicsCount = 0 },
					new Category { Name = "Предложения за платформата", Section = ideas, TopicsCount = 0 },
					new Category { Name = "Съобщаване за проблеми", Section = ideas, TopicsCount = 0 },
					new Category { Name = "Развлечения", Section = other, TopicsCount = 0 },
					new Category { Name = "Похвали се!", Section = other, TopicsCount = 0 }
				};

				foreach (var category in categories)
				{
					context.Categories.Add(category);
				}

				context.SaveChanges();
			}
		}
	}
}
