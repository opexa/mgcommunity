namespace MGCommunity.Services.App_Start
{
	using AutoMapper;
	using MGCommunity.Models;
	using Models.ViewModels;
	using System.Collections.Generic;
	using System.Linq;

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

			// Topic Maps
			CreateMap<Topic, ShortTopicViewModel>()
				.ForMember(model => model.AuthorUsername, cfg => cfg.MapFrom(topic => topic.Author.UserName))
				.ForMember(model => model.LastReply, cfg => cfg.MapFrom(topic => topic.Replies.Last().PostedOn))
				.ForMember(model => model.LastReplyBy, cfg => cfg.MapFrom(topic => topic.Replies.OrderByDescending(r => r.PostedOn).First().Author.UserName));
		}
	}
}