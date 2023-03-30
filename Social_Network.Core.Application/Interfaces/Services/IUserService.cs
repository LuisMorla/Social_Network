using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Services
{
    public interface IUserService:IGenericService<User, UserViewModel, SaveUserViewModel>
    {
        Task<List<UserViewModel>> GetAllViewModelWithInclude();
        Task<UserViewModel> Login(LoginViewModel loginvm);
        Task<bool> ValidateUserName(string UserName);
        Task ChangePassword(string username);
        Task<UserViewModel> GetUserbyUsername(string username);
    }
}
