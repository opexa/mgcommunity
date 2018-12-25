namespace MGCommunity.Services.Controllers
{
	using MGCommunity.Data;
	using MGCommunity.Models;
	using Models.BindingModels;
	using System.Linq;
	using System.Web.Http;
	using UserSessionUtils;

	[RoutePrefix("api/Category")]
	[SessionAuthorize(Roles = "Administrator")]
	public class CategoriesController : BaseApiController
	{
		public CategoriesController(IMGCommunityData data) : base(data) { }

		public CategoriesController() : this(new MGCommunityData()) { }

		// POST api/Category/Create
		[HttpPost]
		[Route("Create")]
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
