using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Domain.Entities;
using Social_Network.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence.Repository
{
    public class PublicationRepository:GenericRepository<Publication>,IPublicationRepository
    {
        private readonly ApplicationContext _context;

        public PublicationRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<List<Publication>> GetAllWithFriends(int friend)
        //{

        //    return await _context.publications
        //        .Include(pok => pok.Comments)
        //        .Include(pok => pok.Users)
        //        .Where(pok => pok.UserId == friend).ToListAsync();
        //}
    }
}
