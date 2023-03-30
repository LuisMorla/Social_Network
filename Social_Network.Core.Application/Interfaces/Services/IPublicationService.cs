using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Services
{
    public interface IPublicationService:IGenericService<Publication, PublicationViewModel, SavePublicationViewModel>
    {
        Task<List<PublicationViewModel>> GetAllWithInclude();
    }
}
