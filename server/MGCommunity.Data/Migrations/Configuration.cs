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
			SeedTopicsAndReplies(context);
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
					Status = "Администратор",
					Email = "admin@mgberon.com"
				};
				var adminUserCreateResult = userManager.Create(adminUser, "mgadmin1298");

				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
				var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
				var roleTeacherCreateResult = roleManager.Create(new IdentityRole("Teacher"));
				var roleUserCreateResult = roleManager.Create(new IdentityRole("Student"));

				if (!roleTeacherCreateResult.Succeeded)
				{
					throw new Exception(string.Join("; ", roleTeacherCreateResult.Errors));
				}

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
					new Category() { Name = "Правила", Section = general, TopicsCount = 3, Description = "Правилата за ползване на платформата." },
					new Category { Name = "Новини", Section = general, TopicsCount = 3, Description = "Всички новини свързани с училище." },
					new Category { Name = "Състезания", Section = general, TopicsCount = 3, Description = "Информация относно предстоящи състезания." },
					new Category { Name = "Предприемачество", Section = groups, TopicsCount = 3, Description = "Свържи се с останалите предприемачи в MG!" },
					new Category { Name = "Физика и Астрономия", Section = groups, TopicsCount = 3, Description = "Клубът на бъщетите физици." },
					new Category { Name = "Спорт", Section = groups, TopicsCount = 3, Description = "Клубът на спортните надежди в МГ." },
					new Category { Name = "Математика", Section = groups, TopicsCount = 3, Description = "Магазин за калкулатори." },
					new Category { Name = "Предложения за платформата", Section = ideas, TopicsCount = 3, Description = "Сподели какво може да подобрим в платформата!" },
					new Category { Name = "Съобщаване за проблеми", Section = ideas, TopicsCount = 3, Description = "Имаш оплакване. Сподели го тук." },
					new Category { Name = "Развлечения", Section = other, TopicsCount = 3, Description = "Забавната част от платформата :)" },
					new Category { Name = "Похвали се!", Section = other, TopicsCount = 3, Description = "Имаш с какво да се похвалиш? Давай смело!" }
				};

				foreach (var category in categories)
				{
					context.Categories.Add(category);
				}

				context.SaveChanges();
			}
		}

		public static void SeedTopicsAndReplies(MGCContext context)
		{
			if (!context.Topics.Any())
			{
				var user1 = context.Users.First();

				var categories = context.Categories.ToList();
				foreach (var category in categories)
				{
					var topic = new Topic
					{
						AuthorId = user1.Id,
						CreatedOn = DateTime.Now,
						Locked = false,
						Pinned = false,
						Title = "Test topic for each category",
						RepliesCount = 1,
						CategoryId = category.Id
					};
					var topic2 = new Topic
					{
						AuthorId = user1.Id,
						CreatedOn = DateTime.Now,
						Locked = false,
						Pinned = false,
						Title = "Another test topic for the same category.",
						RepliesCount = 1,
						CategoryId = category.Id
					};
					var topic3 = new Topic
					{
						AuthorId = user1.Id,
						CreatedOn = DateTime.Now,
						Locked = false,
						Pinned = false,
						Title = "Third test topic for the category.",
						RepliesCount = 1,
						CategoryId = category.Id
					};
					context.Topics.Add(topic);
					context.Topics.Add(topic2);
					context.Topics.Add(topic3);
					context.SaveChanges();

					var reply = new Reply
					{
						AuthorId = user1.Id,
						PostedOn = DateTime.Now,
						TopicId = topic.Id,
						Content = "This is the test reply"
					};
					var reply2 = new Reply
					{
						AuthorId = user1.Id,
						PostedOn = DateTime.Now,
						TopicId = topic2.Id,
						Content = "This is the test reply"
					};
					var reply3 = new Reply
					{
						AuthorId = user1.Id,
						PostedOn = DateTime.Now,
						TopicId = topic3.Id,
						Content = "This is the test reply"
					};
					topic.Participants.Add(user1);
					topic2.Participants.Add(user1);
					topic3.Participants.Add(user1);

					topic.Replies.Add(reply);
					topic2.Replies.Add(reply2);
					topic3.Replies.Add(reply3);
					context.SaveChanges();
				}
			}
		}
	}
}
