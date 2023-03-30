using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Publications
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public string? Picture { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string? ImageUser { get; set; }
        public DateTime? Created { get; set; }

        public UserViewModel? Users { get; set; }
        public ICollection<CommentViewModel>? Comments { get; set; }
    }
}
