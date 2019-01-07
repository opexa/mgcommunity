namespace MGCommunity.Services.Controllers
{
	using Data;
	using UserSessionUtils;
	using Models.BindingModels;
	using System.Linq;
	using System.Web.Http;
	using MGCommunity.Models;
	using System;
	using Microsoft.AspNet.Identity;
	using AutoMapper;
	using System.Collections.Generic;
	using Models.ViewModels;

	[RoutePrefix("api/Topic")]
	[SessionAuthorize]
	public class TopicsController : BaseApiController
	{
		public static readonly int RepliesPerPage = 10;

		public TopicsController(IMGCommunityData data) : base(data) { }

		public TopicsController() : this(new MGCommunityData()) { }

		// GET api/Topic/Info/:id
		[HttpGet]
		[Route("Info")]
		[SessionAuthorize(Roles = "Administrator, Student, Teacher")]
		public IHttpActionResult Info(int id)
		{
			var topic = this.Data.Topics.FindById(id);
			var data = Mapper.Map<TopicInfoViewModel>(topic);

			return this.Ok(data);
		}

		// GET api/Topic/Replies/{id}?page={page}
		[HttpGet]
		[Route("Replies")]
		[SessionAuthorize(Roles = "Administrator, Student, Teacher")]
		public IHttpActionResult Replies(int id, int page)
		{
			var skip = (page - 1) * RepliesPerPage;
			var replies = this.Data.Topics.FindById(id).Replies.OrderBy(r => r.PostedOn).Skip(skip).Take(10);
			var data = Mapper.Map<IEnumerable<ReplyViewModel>>(replies);

			return this.Ok(data);
		}

		// POST api/Topic/Create
		[HttpPost]
		[Route("Create")]
		[SessionAuthorize(Roles = "Administrator, Student, Teacher")]
		public IHttpActionResult Create(CreateTopicBindingModel model)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest("Моля погледнете всички полета още веднъж.");

			// TODO: CHECK IF TOPICS WITH SIMILAR NAME EXIST

			var topicExisting = this.Data.Topics.All().Any(t => t.Title == model.Title);
			if (topicExisting)
				return this.BadRequest("Съществува тема с такова заглавие.");

			var categoryExisting = this.Data.Categories.All().Any(c => c.Id == model.CategoryId);
			if (!categoryExisting)
				return this.BadRequest("Изглежда, че се опитвате да добавите тема към несъществуваща категория.");

			var loggedUserId = this.User.Identity.GetUserId();

			var newTopic = new Topic
			{
				Title = model.Title,
				CreatedOn = DateTime.Now,
				CategoryId = model.CategoryId,
				AuthorId = loggedUserId,
				Locked = false,
				Pinned = false,
				RepliesCount = 1
			};

			var loggedUser = this.Data.Users.FindById(loggedUserId);
			newTopic.Participants.Add(loggedUser);

			this.Data.Categories.FindById(model.CategoryId).TopicsCount += 1;
			this.Data.Topics.Add(newTopic);
			this.Data.SaveChanges();

			var firstReply = new Reply
			{
				AuthorId = loggedUserId,
				PostedOn = DateTime.Now,
				TopicId = newTopic.Id,
				Content = model.FirstReply
			};
			this.Data.Replies.Add(firstReply);
			this.Data.SaveChanges();

			return this.Ok(new { message = "Темата е добавена успешно :) " });
		}

		// POST api/Topic/Pin/{id}
		[HttpPost]
		[Route("Pin")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Pin(int id)
		{
			var topic = this.Data.Topics.All().FirstOrDefault(t => t.Id == id);
			if (topic == null)
			{
				return this.BadRequest("Изглежда, че се опитвате да забодете тема, която не съществува вече.");
			}

			topic.Pinned = true;
			this.Data.Topics.Update(topic);
			this.Data.SaveChanges();
			return this.Ok(new { message = "Темата е забодена :) ." });
		}

		// POST api/Topic/Unpin/{id}
		[HttpPost]
		[Route("Unpin")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Unpin(int id)
		{
			var topic = this.Data.Topics.All().FirstOrDefault(t => t.Id == id);
			if (topic == null)
			{
				return this.BadRequest("Изглежда, че се опитвате да освободите тема, която не съществува вече.");
			}

			topic.Pinned = false;
			this.Data.Topics.Update(topic);
			this.Data.SaveChanges();
			return this.Ok(new { message = "Темата е освободена :) ." });
		}

		// POST api/Topic/Lock/{id}
		[HttpPost]
		[Route("Lock")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Lock(int id)
		{
			var topic = this.Data.Topics.All().FirstOrDefault(t => t.Id == id);
			if (topic == null)
				return this.BadRequest("Изглежада, че темата, която се опитвате да заключите не съществува.");

			topic.Locked = true;
			this.Data.Topics.Update(topic);
			this.Data.SaveChanges();
			return this.Ok(new { message = "Темата е заключена." });
		}

		// POST api/Topic/Unlock/{id}
		[HttpPost]
		[Route("Unlock")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Unlock(int id)
		{
			var topic = this.Data.Topics.All().FirstOrDefault(t => t.Id == id);
			if (topic == null)
				return this.BadRequest("Изглежада, че темата, която се опитвате да отключите не съществува.");

			topic.Locked = false;
			this.Data.Topics.Update(topic);
			this.Data.SaveChanges();
			return this.Ok(new { message = "Темата е отключена." });
		}

		// POST api/Topic/Delete/{id}
		[HttpPost]
		[Route("Delete")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Delete(int id)
		{
			try
			{
				this.Data.Topics.Delete(id);
				this.Data.SaveChanges();
				return this.Ok(new { message = "Темата беше изтрита успешно." });
			}
			catch(Exception ex)
			{
				return this.BadRequest("Възникна някаква грешка. Моля презаредете страницата и опитайте отново.");
			}			
		}
	}
}
