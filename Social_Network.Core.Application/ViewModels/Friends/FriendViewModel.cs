using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int Id { get; set; }
        public int UserFirst { get; set; }
        public int UserSecond { get; set; }
        public string FriendName { get; set; }
        public string FriendImage { get; set; }
        public string? FriendFirstName { get; set; }

        public string? FriendLastName { get; set; }


        public UserViewModel? User1 { get; set; }
        public UserViewModel? User2 { get; set; }

    }
}
