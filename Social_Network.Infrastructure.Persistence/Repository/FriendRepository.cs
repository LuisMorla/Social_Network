using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.ViewModels.Friends;
using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Domain.Entities;
using Social_Network.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence.Repository
{
    public class FriendRepository:GenericRepository<Friend>, IFriendRepository
    {
        private readonly ApplicationContext _context;

        public FriendRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Friend> CheckAreFriend(Friend entity)
        {

            var Validate = await base.GetAllAsyncWithOutInclude();
            return Validate.FirstOrDefault(friend => (friend.UserFirst == entity.UserFirst || friend.UserFirst == entity.UserSecond) && (friend.UserSecond == entity.UserFirst || friend.UserSecond == entity.UserSecond));

        }
        //public async Task<List<Friend>> GetForUserId(int FriendId)
        //{

        //    return await _context.friends
        //        .Include(pok => pok.User2)
        //        .Where(pk => pk.UserSecond == FriendId)
        //        .ToListAsync();

        //}

        //public async Task<List<Friend>> GetForUserId(int FriendId)
        //{

        //    return await _context.friends
        //    .Where(f=>f.UserFirst == FriendId || f.UserSecond == FriendId).ToListAsync();


        //}

        public async Task<List<Friend>> GetAllWithIncludeLinq()
        {

           return await _context.friends
                .Include(f=>f.User1)
                .Include(f=>f.User2)
                .ToListAsync();

        }


    }
}
