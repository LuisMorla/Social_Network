using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Repository
{
    public interface IPublicationRepository : IGenericRepository<Publication>
    {
        //Task<List<Publication>> GetAllWithFriends(int friend);
    }
}
