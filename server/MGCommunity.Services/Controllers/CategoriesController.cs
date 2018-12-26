namespace MGCommunity.Services.Controllers
{
	using AutoMapper;
	using MGCommunity.Data;
	using MGCommunity.Models;
	using Models.BindingModels;
	using Models.ViewModels;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http;
	using UserSessionUtils;

	[RoutePrefix("api/Category")]
	[SessionAuthorize]
	public class CategoriesController : BaseApiController
	{
		public CategoriesController(IMGCommunityData data) : base(data) { }

		public CategoriesController() : this(new MGCommunityData()) { }

		// GET api/Category/Feed/{id}?page={page}
		[HttpGet]
		[Route("Feed")]
		public IHttpActionResult Feed(int id, int page)
		{
			int skip = (page - 1) * 10;
			var topics = this.Data.Categories.FindById(id).Topics.OrderByDescending(t => t.CreatedOn).Skip(skip).Take(10);

			var data = Mapper.Map<IEnumerable<ShortTopicViewModel>>(topics);
			return this.Ok(data);
		}

		// POST api/Category/Create
		[HttpPost]
		[Route("Create")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Create(CreateCategoryBindingModel model)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest("Възникна грешка. Моля опитайте отново.");

			var existingCategory = this.Data.Categories.All().Any(c => c.Name == model.Name);
			if (existingCategory)
				return this.BadRequest("Вече съществува такава категория.");

			var newCategory = new Category
			{
				Name = model.Name,
				SectionId = model.SectionId,
				TopicsCount = 0
			};
			this.Data.Categories.Add(newCategory);
			this.Data.SaveChanges();

			return this.Ok(new { message = "Успешно създадена категория", category = newCategory });
		}

		// POST api/Category/Delete/{id}
		[HttpPost]
		[Route("Delete")]
		[SessionAuthorize(Roles = "Administrator")]
		public IHttpActionResult Delete(int id)
		{
			var category = this.Data.Categories.FindById(id);
			if (category == null)
				return this.BadRequest("Категорията, която се опитвате да изтриете не съществува.");

			this.Data.Categories.Delete(category);
			this.Data.SaveChanges();
			
			return this.Ok(new { message = "Категорията е изтрита." });
		}
	}
}
