using Social_Network.Core.Application.ViewModels.Friends;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Services
{
    public interface IFriendService:IGenericService<Friend, FriendViewModel, SaveFriendViewModel>
    {
        Task<FriendViewModel> CheckAreFriend(SaveFriendViewModel vm);
        Task<List<FriendViewModel>> GetAllViewModelWithInclude();
    }
}
