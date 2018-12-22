using MGCommunity.Data;
using MGCommunity.Models;
using MGCommunity.Services.App_Start;
using MGCommunity.Services.Models.BindingModels;
using MGCommunity.Services.UserSessionUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MGCommunity.Services.Controllers
{
	[SessionAuthorize(Roles = "Administrator")]
	[RoutePrefix("api/Section")]
	public class SectionsController : BaseApiController
	{
		public SectionsController(IMGCommunityData data) : base(data) { }

		public SectionsController() : base(new MGCommunityData()) { }

		[HttpPost]
		[Route("Create")]
		// POST api/Section/Create
		public IHttpActionResult Create([FromBody]CreateSectionBindingModel model)
		{
			if (model.Name == null)
			{
				return this.BadRequest("Не сте въвели име на секцита");
			}

			if (this.Data.Sections.All().Any(s => s.Name == model.Name))
			{
				return BadRequest("Вече съществува такава секция.");
			}

			Section section = new Section { Name = model.Name, Visible = true };
			this.Data.Sections.Add(section);
			this.Data.SaveChanges();

			return this.Ok(new { message = "Секцията е успешно създадена" });
		}

		[HttpPost]
		[Route("Hide")]
		// POST api/Section/Hide/{id}
		public IHttpActionResult Hide(int id)
		{
			Section section = this.Data.Sections.FindById(id);
			if (section == null)
			{
				return this.BadRequest("Секцията, която се опитвате да скриете не съществува.");
			}

			section.Visible = false;
			this.Data.SaveChanges();
			return this.Ok(new { message = "Секцията е скрита" });
		}

		[HttpPost]
		[Route("Show")]
		// POST api/Section/Show/{id}
		public IHttpActionResult Show(int id)
		{
			Section section = this.Data.Sections.FindById(id);
			if (section == null)
			{
				return this.BadRequest("Секцията, която се опитвате да покажете не съществува.");
			}

			section.Visible = true;
			this.Data.SaveChanges();
			return this.Ok(new { message = "Секцията е показана" });
		}
	}
}
