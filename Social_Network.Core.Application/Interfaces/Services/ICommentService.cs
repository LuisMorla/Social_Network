using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Services
{
    public interface ICommentService:IGenericService<Comment, CommentViewModel, SaveCommentViewModel>
    {
        Task<List<CommentViewModel>> GetAllViewModelWithInclude();
    }
}
