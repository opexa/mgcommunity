namespace MGCommunity.Services.App_Start
{
	using AutoMapper;
	using MGCommunity.Models;
	using Models.ViewModels;
	using System.Collections.Generic;
	using System.Linq;
	using MGCommunity.Services.App_Start;

	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			// Account Maps
			CreateMap<User, AccountDetailsViewModel>();

			// Reply Maps
			CreateMap<Reply, ReplyViewModel>()
				.ForMember(model => model.AuthorUsername, cfg => cfg.MapFrom(reply => reply.Author.UserName))
				.ForMember(model => model.AuthorStatus, cfg => cfg.MapFrom(reply => reply.Author.Status))
				.ForMember(model => model.AuthorAvatar, cfg => cfg.MapFrom(reply => reply.Author.Avatar))
				.ForMember(model => model.RelativeDate, cfg => cfg.MapFrom(reply => reply.PostedOn.ToRelativeDateString()))
				.ForMember(model => model.DetailedDate, cfg => cfg.MapFrom(reply => reply.PostedOn.ToString("dddd, dd MMMM yyyy HH:mm")))
				.ForMember(model => model.LikesCount, cfg => cfg.MapFrom(reply => reply.Likes.Count))
				.ForMember(model => model.LastThreeLikes, cfg => cfg.MapFrom(reply => Mapper.Map<IEnumerable<LikeViewModel>>(reply.Likes.Reverse().Take(3))));

			// Like Maps
			CreateMap<User, LikeViewModel>()
				.ForMember(model => model.LikerUsername, cfg => cfg.MapFrom(user => user.UserName))
				.ForMember(model => model.LikerAvatar, cfg => cfg.MapFrom(user => user.Avatar));

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
				.ForMember(model => model.AuthorAvatar, cfg => cfg.MapFrom(topic => topic.Author.Avatar))
				.ForMember(model => model.ParticipantsCount, cfg => cfg.MapFrom(topic => topic.Participants.Count))
				.ForMember(model => model.CreatedOn, cfg => cfg.MapFrom(topic => topic.CreatedOn.ToRelativeDateString()))
				.ForMember(model => model.LastReply, cfg => cfg.MapFrom(topic => topic.Replies.OrderByDescending(r => r.PostedOn).First().PostedOn.ToRelativeDateString()))
				.ForMember(model => model.LastReplyBy, cfg => cfg.MapFrom(topic => topic.Replies.OrderByDescending(r => r.PostedOn).First().Author.UserName));

			CreateMap<Topic, TopicInfoViewModel>()
				.ForMember(model => model.AuthorUsername, cfg => cfg.MapFrom(topic => topic.Author.UserName))
				.ForMember(model => model.CreatedOn, cfg => cfg.MapFrom(topic => topic.CreatedOn.ToRelativeDateString()))
				.ForMember(model => model.MaxPage, cfg => cfg.MapFrom(topic => topic.Replies.Count % 10 == 0 ? topic.Replies.Count /10 : topic.Replies.Count / 10 + 1 ));
		}
	}
}