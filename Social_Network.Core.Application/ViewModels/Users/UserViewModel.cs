using Social_Network.Core.Application.ViewModels.Comments;
using Social_Network.Core.Application.ViewModels.Friends;
using Social_Network.Core.Application.ViewModels.Publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }


        public string Email { get; set; }

        public string Phone { get; set; }

        public string ImageUser { get; set; }
        public bool IsVerified { get; set; }

        public ICollection<PublicationViewModel>? publicaciones { get; set; }
        public ICollection<CommentViewModel>? Comments { get; set; }

        public ICollection<FriendViewModel>? friend1 { get; set; }
        public ICollection<FriendViewModel>? friend2 { get; set; }


    }
}
