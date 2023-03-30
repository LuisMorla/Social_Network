using AutoMapper;
using Microsoft.AspNetCore.Http;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.Interfaces.Services;
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

namespace Social_Network.Core.Application.Services
{
    public class FriendService:GenericService<Friend, FriendViewModel, SaveFriendViewModel>, IFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;

        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;
        private readonly UserViewModel userViewModel;
        private readonly IHttpContextAccessor _httpContext;

        public FriendService(IFriendRepository friendRepository, IMapper mapper, IHttpContextAccessor httpContext, IPublicationRepository publicationRepository, ICommentRepository commentRepository, IUserRepository userRepository) : base(friendRepository, mapper)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            userViewModel = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<FriendViewModel> CheckAreFriend(SaveFriendViewModel vm)
        {

            var Validate = await _friendRepository.CheckAreFriend(_mapper.Map<Friend>(vm));

            if (Validate == null)
            {
                return null;
            }

            return _mapper.Map<FriendViewModel>(Validate);
        }

        public async Task<List<FriendViewModel>> GetAllViewModelWithInclude()
        {

            var List = await _friendRepository.GetAllWithIncludeLinq();
            return List.Where(x=>x.UserFirst == userViewModel.Id || x.UserSecond == userViewModel.Id).Select(friends => new FriendViewModel
            {
                Id = friends.Id,
                UserFirst = friends.UserFirst,
                UserSecond = friends.UserSecond,
                FriendName = friends.User2.UserName == userViewModel.Username ? friends.User1.UserName : friends.User2.UserName,
                FriendImage = friends.User2.ImageUser == userViewModel.ImageUser ? friends.User1.ImageUser : friends.User2.ImageUser,
                FriendFirstName = friends.User2.Name == userViewModel.Name ? friends.User1.Name : friends.User2.Name,
                FriendLastName = friends.User2.LastName == userViewModel.LastName ? friends.User1.LastName : friends.User2.LastName


            }).ToList();
        }
    }
}
