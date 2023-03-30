using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Application.Helpers;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using Social_Network.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence.Repository
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        public override async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);

            return await base.AddAsync(entity);
        }

        public async Task<User> LoginAsync(LoginViewModel loginvm)
        {
            string passwordEncrypt = PasswordEncryption.ComputeSha256Hash(loginvm.Password);
            User user = await _context.Set<User>().FirstOrDefaultAsync(user => user.UserName == loginvm.Username && user.Password == passwordEncrypt);
            return user;
        }

        public async Task<User> ValidateUser(string Username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserName == Username);
        }
    }
}
