namespace MGCommunity.Services.App_Start
{
	using AutoMapper;
	using MGCommunity.Models;
	using Models.ViewModels;
	using System.Collections.Generic;

	public class MapperConfig : Profile
	{
		public MapperConfig()
		{

			// Category Maps
			CreateMap<Category, ShortCategoryViewModel>();

			// Section Maps
			CreateMap<Section, HomeSectionViewModel>()
				.ForMember(model => model.Categories, cfg => {
					cfg.MapFrom(section => Mapper.Map<IEnumerable<ShortCategoryViewModel>>(section.Categories));
				});
		}
	}
}