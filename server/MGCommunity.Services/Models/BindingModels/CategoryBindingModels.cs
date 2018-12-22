namespace MGCommunity.Services.Models.BindingModels
{
	using System.ComponentModel.DataAnnotations;

	public class CreateCategoryBindingModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public int SectionId { get; set; }
	}
}