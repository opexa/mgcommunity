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

	[RoutePrefix("api/Topic")]
	[SessionAuthorize(Roles = "Administrator")]
	public class TopicsController : BaseApiController
	{
		public TopicsController(IMGCommunityData data) : base(data) { }

		public TopicsController() : this(new MGCommunityData()) { }

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
