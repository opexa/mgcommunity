namespace MGCommunity.Services.Controllers
{
	using AutoMapper;
	using MGCommunity.Data;
	using MGCommunity.Services.Models.ViewModels;
	using MGCommunity.Services.UserSessionUtils;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Web.Http;

	[RoutePrefix("api/Home")]
	public class HomeController : BaseApiController
	{
		public HomeController(IMGCommunityData data) : base(data) { }

		public HomeController() : this(new MGCommunityData()) { }

		[HttpGet]
		[Route("GetSections")]
		[SessionAuthorize]
		// GET api/Home/GetSections
		public IHttpActionResult GetSections()
		{
			var sections = this.Data.Sections.All().Include(s => s.Categories);

			if (this.User.IsInRole("Student") || this.User.IsInRole("Teacher"))
				sections = sections.Where(s => s.Visible == true);

			var data = Mapper.Map<IEnumerable<HomeSectionViewModel>>(sections);

			return this.Ok(data);
		}
	}
}
