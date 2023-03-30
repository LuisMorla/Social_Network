using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginvm);
        //Task<bool> ValidateUser(string Username);

        Task<User> ValidateUser(string Username);
    }
}
