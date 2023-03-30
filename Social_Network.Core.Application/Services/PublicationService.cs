using AutoMapper;
using Microsoft.AspNetCore.Http;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.ViewModels.Comments;
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
    public class PublicationService:GenericService<Publication,PublicationViewModel,SavePublicationViewModel>, IPublicationService
    {
        private readonly IPublicationRepository _publication;
        private readonly ICommentRepository _CommentRepository;
        private readonly IFriendRepository _friendRepository;

        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;

        public PublicationService(IPublicationRepository publication, IHttpContextAccessor httpContext, IMapper mapper, ICommentRepository commentRepository, IFriendRepository friendRepository) : base(publication, mapper)
        {
            _publication = publication;
            _httpContext = httpContext;
            userViewModel = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
            _CommentRepository = commentRepository;
            _friendRepository = friendRepository;
        }

        public override async Task<SavePublicationViewModel> Add(SavePublicationViewModel vm)
        {
            vm.UserId = userViewModel.Id;
            vm.ImageUser = userViewModel.ImageUser;
            vm.UserName = userViewModel.Username;

            return await base.Add(vm);

        }


        public override async Task Update(SavePublicationViewModel vm, int id)
        {
            vm.UserId = userViewModel.Id;
            vm.UserName = userViewModel.Username;

            await base.Update(vm, id);
        }

        public async Task<List<PublicationViewModel>> GetAllWithInclude()
        {
            var todo = await _CommentRepository.GetAllAsyncWithOutInclude();

            var list = await _publication.GetAllAsyncWithInclude(new List<string> { "Users", "Comments" });


            return list.Where(p => p.UserId == userViewModel.Id).Select(x => new PublicationViewModel
            {
                Id = x.Id,
                Caption = x.Caption,
                Picture = x.Picture,
                UserId= x.UserId,
                Comments = _mapper.Map<List<CommentViewModel>>(todo.ToList().Where(p => p.PublicationId == x.Id).ToList()),
                ImageUser = x.ImageUser,
                UserName = x.UserName,
                Created = x.Created
            }).ToList();
        }


    }
}
