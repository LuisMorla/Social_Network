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
    public class CommentService:GenericService<Comment, CommentViewModel, SaveCommentViewModel>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel userViewModel;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IPublicationRepository _publicationRepository;



        public CommentService(ICommentRepository commentRepository, IMapper mapper, IUserRepository repository, IHttpContextAccessor httpContext, IPublicationRepository publicationRepository) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _repository = repository;
            _httpContext = httpContext;
            userViewModel = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _publicationRepository = publicationRepository;

        }

        public async Task<List<CommentViewModel>> GetAllViewModelWithInclude()
        {

            var List = await _commentRepository.GetAllAsyncWithInclude(new List<string> { "publication", "user" });
            return List.Select(comment => new CommentViewModel
            {
                Id= comment.Id,
                Caption = comment.Caption,
                UserId = comment.UserId,
                PublicationId = comment.PublicationId,
                UserImage =  _repository.GetByIdAsync(comment.UserId).ContinueWith(u=>u.Result.ImageUser).Result,
                UserName = _repository.GetByIdAsync(comment.UserId).ContinueWith(u => u.Result.UserName).Result,

            }).ToList();
        }
    }
}
