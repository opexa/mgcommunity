namespace MGCommunity.Services.Controllers
{
	using Data;
	using MGCommunity.Models;
	using Microsoft.AspNet.Identity;
	using Models.BindingModels;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Web.Http;
	using UserSessionUtils;

	[RoutePrefix("api/Reply")]
	[SessionAuthorize(Roles = "Administrator, Student")]
	public class RepliesController : BaseApiController
	{
		public RepliesController(IMGCommunityData data) : base(data) { }

		public RepliesController() : this(new MGCommunityData()) { }

		// POST api/Reply/Add
		[HttpPost]
		[Route("Add")]
		public IHttpActionResult Add(AddReplyBindingModel model)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest("Моля уверете се, че сте написали коментар.");

			var loggedUserId = this.User.Identity.GetUserId();

			var newReply = new Reply
			{
				AuthorId = loggedUserId,
				Content = model.Content,
				PostedOn = DateTime.Now,
				TopicId = model.TopicId
			};
			
			this.Data.Replies.Add(newReply);

			var topic = this.Data.Topics.FindById(model.TopicId);
			var user = this.Data.Users.FindById(loggedUserId);
			topic.Participants.Add(user);
			
			this.Data.SaveChanges();

			return this.Ok(new { message = "Коментарът беше публикуван успешно", reply = newReply });
		}

		// PUT api/Reply/Edit
		[HttpPut]
		[Route("Edit")]
		public IHttpActionResult Edit(EditReplyBindingModel model)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest("Коментарът, който се опитвате да редактирате не съществува.");

			var reply = this.Data.Replies.FindById(model.ReplyId);
			if (reply == null)
				return this.BadRequest("Моля опитайте да редактирате коментара отново.");

			var loggedUserId = this.User.Identity.GetUserId();
			if (reply.AuthorId != loggedUserId && !this.User.IsInRole("Adminstrator"))
				return this.Unauthorized();

			reply.Content = model.Content;
			this.Data.Replies.Update(reply);
			this.Data.SaveChanges();

			return this.Ok(new { message = "Успешно редактиран коментар." });
		}

		// PUT api/Reply/Like
		[HttpPut]
		[Route("Like")]
		public IHttpActionResult Like(int id)
		{
			var reply = this.Data.Replies.FindById(id);
			if (reply == null)
				return this.BadRequest("Упсс.. Нещо се обърка :/ Моля, опитайте отново.");

			var loggedUserId = this.User.Identity.GetUserId();

			var isLiked = this.Data.Likes.All().FirstOrDefault(l => l.ReplyId == id && l.UserId == loggedUserId);
			if (isLiked != null)
			{
				this.Data.Likes.Delete(isLiked);
				this.Data.SaveChanges();
				return this.Ok(new { status = "Unliked" });
			}
			else
			{
				var newLike = new Like
				{
					UserId = loggedUserId,
					ReplyId = id
				};
				this.Data.Likes.Add(newLike);
				this.Data.SaveChanges();
				return this.Ok(new { status = "Liked" });
			}
		}
	}
}
