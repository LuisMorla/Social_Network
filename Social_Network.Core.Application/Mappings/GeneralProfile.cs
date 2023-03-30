using AutoMapper;
using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Application.ViewModels.Friends;
using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserProfile
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))

                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<User, SaveUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Validate, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Publications, opt => opt.Ignore())
                .ForMember(x => x.Friends1, opt => opt.Ignore())
                .ForMember(x => x.Friends2, opt => opt.Ignore());

            #endregion

            #region PublicationProfile
            CreateMap<Publication, PublicationViewModel>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments))
                .ForMember(x => x.Created, opt => opt.MapFrom(x=>x.Created))
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Publication, SavePublicationViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.Users, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region CommentsProfile
            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.user, opt => opt.MapFrom(x => x.user))
                .ForMember(x => x.publication, opt => opt.MapFrom(x => x.publication))
                .ForMember(x => x.UserImage, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.user, opt => opt.MapFrom(x => x.user))
                .ForMember(x => x.publication, opt => opt.MapFrom(x => x.publication))
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Comment, SaveCommentViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.user, opt => opt.Ignore())
                .ForMember(x => x.publication, opt => opt.Ignore());

            #endregion

            #region FriendProfile
            CreateMap<Friend, FriendViewModel>()
                .ForMember(x => x.FriendImage, opt => opt.Ignore())
                .ForMember(x => x.FriendName, opt => opt.Ignore())
                .ForMember(x => x.FriendFirstName, opt => opt.Ignore())
                .ForMember(x => x.FriendLastName, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Friend, SaveFriendViewModel>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.User1, opt => opt.Ignore())
                .ForMember(x => x.User2, opt => opt.Ignore());

            #endregion
        }
    }
}
