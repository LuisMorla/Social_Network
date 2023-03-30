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
    public class CommentRepository:GenericRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationContext _context;
        public CommentRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }
    }
}
