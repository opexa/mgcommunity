using MGCommunity.Data;
using MGCommunity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace MGCommunity.Services.Controllers
{
	public class BaseApiController : ApiController
	{
		protected IMGCommunityData Data { get; set; }

		public BaseApiController(IMGCommunityData data)
		{
			this.Data = data;
		}

		protected IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return this.InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						this.ModelState.AddModelError(string.Empty, error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return this.BadRequest();
				}

				return this.BadRequest(this.ModelState);
			}

			return null;
		}

		protected int RegisterException(Exception ex)
		{
			ExceptionEntry exEntry = new ExceptionEntry
			{
				OccuredOn = DateTime.Now,
				ExceptionData = ex.Data,
				HelpLink = ex.HelpLink,
				Source = ex.Source
			};
			//this.Data.ErrorsLog.Add(exEntry);
			return this.Data.SaveChanges();
		}
	}
}
